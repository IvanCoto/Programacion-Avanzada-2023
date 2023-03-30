using Inventario.Application.Contracts.Identity;
using Inventario.Domain.EntityModels.Identity;
using Inventario.Identity.DbContexts;
using Inventario.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Identity
{
    public static class Injection
    {
        public static IServiceCollection AddIdentityServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer
                (configuration.GetConnectionString("DefaultConnection"));

            });
            services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddScoped<IAccountService, AccountService>();

            return services;
        }

    }
}
