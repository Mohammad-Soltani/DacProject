using DacProject.Application.Interfaces.Persistence;
using DacProject.Domain.Entities;
using DacProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DacProject.Infrastructure.Repositories
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserAuthenticationRepository> _logger;
        public UserAuthenticationRepository(UserDbContext userDbContext, IConfiguration configuration, 
                                            ILogger<UserAuthenticationRepository> logger)
        {
            _userDbContext = userDbContext;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Authenticate(UserLogin user)
        {
            try
            {
                var loggedInUser = await _userDbContext.Users.Where(q => q.Email.ToUpper().Equals(user.Email.ToUpper())
                                                  && q.Password.ToUpper().Equals(user.Password.ToUpper())).FirstOrDefaultAsync();
                if (loggedInUser is not null)
                {
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, loggedInUser.Email),
                    new Claim(ClaimTypes.Role, loggedInUser.Role)
                };

                    var token = new JwtSecurityToken
                        (
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(20),
                            notBefore: DateTime.UtcNow,
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256Signature)
                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return tokenString;
                }
                else
                    return String.Empty;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return String.Empty;
            }
        }
    }
}
