using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.MockAPI;
using PaymentGateway.Application.Services;
using PaymentGateway.Services.MockAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IMockPaymentApi, MockPaymentApi>();
            return services;
        }

    }
}
