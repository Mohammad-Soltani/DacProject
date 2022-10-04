using DacProject.Application.Interfaces.Persistence;
using DacProject.Domain.Entities;
using DacProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DacProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(UserDbContext userDbContext, ILogger<UserRepository> logger)
        {
            _userDbContext = userDbContext;
            _logger = logger;
        }
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _userDbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _userDbContext.Users.Where(q => q.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _userDbContext.Users.Where(q => q.Email.ToUpper().Equals(email.ToUpper())).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddOrUpdateUser(User user)
        {
            try
            {
                var res = false;
                if (user.Id > 0)
                {
                    _userDbContext.Users.Update(user);
                }
                else
                {
                    _userDbContext.Users.Add(user);
                }
                await _userDbContext.SaveChangesAsync();
                res = true;
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    var user = await _userDbContext.Users.Where(q => q.Id == id).FirstOrDefaultAsync();
                    _userDbContext.Users.Remove(user);
                    await _userDbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
