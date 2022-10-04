using DacProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DacProject.Application.Interfaces.Persistence
{
    public interface IUserAuthenticationRepository
    {
        Task<string> Authenticate(UserLogin user);
    }
}
