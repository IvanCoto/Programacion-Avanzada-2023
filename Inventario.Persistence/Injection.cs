using Inventario.Application.Contracts.DbContexts;
using Inventario.Application.Contracts.Repositories;
using Inventario.Persistence.DbContexts;
using Inventario.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Persistence
{
    public static class Injection
    {
        public static IServiceCollection AddPersistenceServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer
                (configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>
                (options => options.GetService<ApplicationDbContext>());

            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }

    }
}
