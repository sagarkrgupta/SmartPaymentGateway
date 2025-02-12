﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
            //services.AddScoped<RabbitMqConsumer>();

            services.AddHostedService<RabbitMqConsumerService>();

            return services;
        }
    }
}
