using DacProject.Application.Interfaces.Persistence;
using DacProject.Application.Interfaces.Services;
using DacProject.Application.Services;
using DacProject.Infrastructure.Data;
using DacProject.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Register Configuration
ConfigurationManager configuration = builder.Configuration;

//Add Database Services
builder.Services.AddDbContext<UserDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    mig => mig.MigrationsAssembly("DacProject.API")));
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//Add Jwt Services

builder.Services.AddAuthentication(authOption =>
{
    authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOption =>
{
    jwtOption.SaveToken = true;
    jwtOption.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
    };
});
builder.Services.AddScoped(typeof(IUserAuthenticationRepository), typeof(UserAuthenticationRepository));
builder.Services.AddScoped(typeof(IUserAuthenticationService), typeof(UserAuthenticationService));

builder.Services.AddAuthorization();

//Add Controllers
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
                               {
                                   option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                   {
                                       Scheme = "Bearer",
                                       BearerFormat = "JWT",
                                       In = ParameterLocation.Header,
                                       Name = "Authorization",
                                       Description = "Authentication with JWT",
                                       Type = SecuritySchemeType.Http
                                   });
                                   option.AddSecurityRequirement(new OpenApiSecurityRequirement
                                   {
                                       {
                                           new OpenApiSecurityScheme
                                           {
                                               Reference = new OpenApiReference
                                               {
                                                   Id ="Bearer",
                                                   Type = ReferenceType.SecurityScheme
                                               } 
                                           },
                                           new List<string>()
                                       }
                                   });
                                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
