using DacProject.Application.Interfaces.Services;
using DacProject.Application.Interfaces.Persistence;
using AutoMapper;
using DacProject.Application.Models;
using DacProject.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DacProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<UserViewModel>> GetAllUsers()
        {
            try
            {
                var res = await _userRepository.GetAllUsers();
                return _mapper.Map<List<UserViewModel>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<UserViewModel> GetUserById(int id)
        {
            try
            {
                var res = await _userRepository.GetUserById(id);
                return _mapper.Map<UserViewModel>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<UserViewModel> GetUserByEmail(string email)
        {
            try
            {
                var res = await _userRepository.GetUserByEmail(email);
                return _mapper.Map<UserViewModel>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddOrUpdateUser(UserViewModel model)
        {
            try
            {
                var userModel = _mapper.Map<User>(model);
                return await _userRepository.AddOrUpdateUser(userModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }
    }
}
