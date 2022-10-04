using DacProject.Domain.Entities;

namespace DacProject.Application.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> GetUserByEmail(string email);

        Task<bool> AddOrUpdateUser(User user);

        Task<bool> DeleteUser(int id);
    }
}
