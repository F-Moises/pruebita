using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUser(UsuarioPersonalizado usuario);
        Task<int> AddVerificationCode(UsuarioPersonalizado usuario);
    }
}
