using AutoMapper;
using DacProject.Application.Models;
using DacProject.Domain.Entities;

namespace DacProject.Application.Common
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<UserLogin, UserLoginViewModel>();
            CreateMap<UserLoginViewModel, UserLogin>();
        }
    }
}
