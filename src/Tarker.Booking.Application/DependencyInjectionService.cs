using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tarker.Booking.Application.Configuration;

namespace Tarker.Booking.Application
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(MapperProfile));

            return services;
        }
    }
}
