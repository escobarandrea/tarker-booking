using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Tarker.Booking.Application.Database;
using Tarker.Booking.Persistence.Database;

namespace Tarker.Booking.Persistence
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseService>(options => options.UseSqlServer(configuration["SQLConnectionString"]));
            services.AddScoped<IDatabaseService, DatabaseService>();


            string GetEnvironmentVariable(string key) => Environment.GetEnvironmentVariable(key) ?? string.Empty;

            if (GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "local")
            {
                string clientId = GetEnvironmentVariable("clientId");
                string tenantId = GetEnvironmentVariable("tenantId");
                string clientSecret = GetEnvironmentVariable("clientSecret");

                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                
                var azureKeyVaultProvider = new SqlColumnEncryptionAzureKeyVaultProvider(credential);
                SqlConnection.RegisterColumnEncryptionKeyStoreProviders(new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>(1, StringComparer.OrdinalIgnoreCase)
                {
                    { SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, azureKeyVaultProvider  }
                });
            }
            else
            {
                var azureKeyVaultProvider = new SqlColumnEncryptionAzureKeyVaultProvider(new ManagedIdentityCredential());
                SqlConnection.RegisterColumnEncryptionKeyStoreProviders(new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>(1, StringComparer.OrdinalIgnoreCase)
                {
                    { SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, azureKeyVaultProvider  }
                });
            }

            return services;
        }
    }
}
