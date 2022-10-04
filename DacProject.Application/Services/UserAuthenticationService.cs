using DacProject.Application.Interfaces.Services;
using DacProject.Application.Interfaces.Persistence;
using AutoMapper;
using DacProject.Application.Models;
using DacProject.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DacProject.Application.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserAuthenticationService> _logger;
        public UserAuthenticationService(IUserAuthenticationRepository userAuthenticationRepository, IMapper mapper,
            ILogger<UserAuthenticationService> logger)
        {
            _userAuthenticationRepository = userAuthenticationRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<string> Authenticate(UserLoginViewModel user)
        {
            try
            {
                var entityModel = _mapper.Map<UserLogin>(user);
                return await _userAuthenticationRepository.Authenticate(entityModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return string.Empty;
            }
        }
    }
}
