using DacProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DacProject.Application.Interfaces.Services
{
    public interface IUserAuthenticationService
    {
        Task<string> Authenticate(UserLoginViewModel user);
    }
}
