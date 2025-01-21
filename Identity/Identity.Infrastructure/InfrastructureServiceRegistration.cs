using Identity.Application.Contracts;
using Identity.Application.Contracts.Repositories;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Infrastructure.Context;
using Identity.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<IdentityContextPostgres>(options =>
                options.UseNpgsql(configuration.GetConnectionString("conexion")));

            services.AddAuthorization();

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<IdentityContextPostgres>()
            //    .AddDefaultTokenProviders();

            services.AddIdentityApiEndpoints<UsuarioPersonalizado>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = true;
            })
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<IdentityContextPostgres>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBrockerMessage, KafkaBrocker>();
            return services;
        }
    }
}
