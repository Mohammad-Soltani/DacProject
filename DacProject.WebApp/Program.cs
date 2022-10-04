using DacProject.WebApp.Helper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDacProjectAPI, DacProjectAPI>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();

    endpoints.MapGet("/", context =>
    {
        return Task.Run(() => context.Response.Redirect("/Account/Login"));
    });
});

app.MapRazorPages();

app.Run();

