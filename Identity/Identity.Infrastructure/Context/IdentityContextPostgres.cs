using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Context
{
    public class IdentityContextPostgres : IdentityDbContext<UsuarioPersonalizado>
    {
        public IdentityContextPostgres(DbContextOptions<IdentityContextPostgres> options) : base(options) { }

        //public DbSet<UsuarioPersonalizado> usuarioPersonalizados { get; set; }
    }
}
