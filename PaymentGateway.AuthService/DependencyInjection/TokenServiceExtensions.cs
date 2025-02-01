using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.AuthService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.AuthService.DependencyInjection
{
    public static class TokenServiceExtensions
    {
        public static IServiceCollection AddTokenService(this IServiceCollection services, string secretKey, string issuer, string audience)
        {
            services.AddSingleton<TokenService>(new TokenService(secretKey, issuer, audience,expiryInMinutes: 720));
            return services;
        }

    }
}
