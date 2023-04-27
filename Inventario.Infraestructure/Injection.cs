using Inventario.Application.Contracts.DbContexts;
using Inventario.Application.Contracts.Identity;
using Inventario.Application.Contracts.Inventario;
using Inventario.Application.Contracts.Recaptcha;
using Inventario.Domain.ConfigurationModels;
using Inventario.Domain.EntityModels;
using Inventario.Infraestructure.Implementations;
using Inventario.Infraestructure.Recaptcha;
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

namespace Inventario.Infrastructure
{
    public static class Injection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
            (options => options.UseSqlServer
            (configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>
            (options => options.GetService<ApplicationDbContext>());

            services.Configure<GoogleRecaptchaSettings>
                (options =>
                    {
                        var settings =
                            configuration.GetSection("GoogleRecaptchaSettings")
                                .Get<GoogleRecaptchaSettings>();
                        options.SiteKey = settings.SiteKey;
                        options.SecretKey = settings.SecretKey;
                        options.VerifyUrl = settings.VerifyUrl;

                    }
                );
            services.AddTransient<IGoogleRecaptchaService, GoogleRecaptchaService>();
            services.AddScoped<IClienteClient, ClienteClient>();
            services.AddScoped<IProveedorClient, ProveedorClient>();
            services.AddScoped<IProductoClient, ProductoClient>();


            services.AddUnitOfWork<ApplicationDbContext>()
                .AddRepository<Cliente, ClienteRepository>()
                .AddRepository<Proveedor,ProveedorRepository>()
                .AddRepository<Producto, ProductoRepository>()
                .AddRepository<Venta, VentaRepository>();


            return services;
        }
    }
}
