using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;
using Identity.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<UsuarioPersonalizado> _context;
        private IdentityContextPostgres _identityContextPostgres;

        public UserRepository(UserManager<UsuarioPersonalizado> context, IdentityContextPostgres identityContextPostgres)
        {
            _context = context;
            _identityContextPostgres = identityContextPostgres;
        }

        public async Task<bool> AddUser(UsuarioPersonalizado usuario)
        {
            string pass = usuario.PasswordHash!;
            usuario.PasswordHash = null;
            IdentityResult result = await _context.CreateAsync(usuario);
            
            if (result.Succeeded)
            {
                var changed = await _context.AddPasswordAsync(usuario, pass!);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> AddVerificationCode(UsuarioPersonalizado usuario)
        {
            Random rnd = new Random();  
            int code = rnd.Next(1000, 10000);
            UsuarioPersonalizado usuarioPersonalizado = await _identityContextPostgres.Users.FirstOrDefaultAsync(x => x.UserName == usuario.UserName);
            if (usuarioPersonalizado != null)
            {
                usuarioPersonalizado!.VerificationCode = code.ToString();
                await _identityContextPostgres.SaveChangesAsync();
                return code;
            }
            else { return -1; }
        }
    }
}
