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
                options.ClientId = "1028536130760-96esa59v8n8gq1j3n7un1htde34n7qck.apps.googleusercontent.com";
                options.ClientSecret = "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";

                //options.CallbackPath = "/Account/ExternalLoginCallBack";
            });

            services.AddScoped<IAccountService, AccountService>();

            return services;
        }

    }
}
