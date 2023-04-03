using Inventario.Application.Contracts.Services;
using Inventario.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application
{
    public static class Injection
    {
        public static IServiceCollection AddApplicationServices
           (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClienteService, ClienteService>()
                .AddScoped<IProveedorService, ProveedorService>();
            return services;

        }
    }
}
