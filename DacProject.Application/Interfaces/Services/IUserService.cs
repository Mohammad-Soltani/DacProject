using DacProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DacProject.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllUsers();

        Task<UserViewModel> GetUserById(int id);

        Task<UserViewModel> GetUserByEmail(string email);

        Task<bool> AddOrUpdateUser(UserViewModel user);

        Task<bool> DeleteUser(int id);
    }
}
