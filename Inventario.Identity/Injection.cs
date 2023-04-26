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
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddGoogle(options =>
            {
                options.ClientId = "464790159146-p7i7qof4v2dnv933m6ab82tcmghf7nqe.apps.googleusercontent.com";
                options.ClientSecret = "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";

                //options.CallbackPath = "/Account/ExternalLoginCallBack";
            });

            services.AddScoped<IAccountService, AccountService>();

            return services;
        }

    }
}
