using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Contract;
using Notification.Application.Service;
using Notification.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBrockerMessage, KafkaBrocker>();
            services.AddSingleton<IMessageEmail, EmailGmail>();     
            return services;
        }
    }
}
