using Inventario.Application.Contracts.Recaptcha;
using Inventario.Domain.ConfigurationModels;
using Inventario.Infraestructure.Recaptcha;
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

            return services;
        }
    }
}
