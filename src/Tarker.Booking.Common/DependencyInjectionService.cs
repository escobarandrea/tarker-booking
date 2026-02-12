using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Common
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddCommon(this IServiceCollection services) => services;
    }
}
