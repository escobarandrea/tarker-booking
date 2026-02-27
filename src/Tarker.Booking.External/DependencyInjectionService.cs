using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tarker.Booking.Application.External.Email;
using Tarker.Booking.Application.External.GetTokenJwt;
using Tarker.Booking.External.Email;
using Tarker.Booking.External.GetTokenJwt;

namespace Tarker.Booking.External
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISendAzureEmailService, SendAzureEmailService>();
            services.AddSingleton<IGetTokenJwtService, GetTokenJwtService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyJwt"]!)),
                    ValidIssuer = configuration["IssuerJwt"],
                    ValidAudience = configuration["AudienceJwt"]
                };
            });

            services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
            {
                ConnectionString = configuration["ApplicatinInsightsConnectionString"]
            });

            return services;
        }
    }
}
