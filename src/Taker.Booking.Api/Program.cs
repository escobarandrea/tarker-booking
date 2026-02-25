using Azure.Identity;
using Tarker.Booking.Api;
using Tarker.Booking.Application;
using Tarker.Booking.Common;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApi().AddCommon().AddApplication().AddExternal(builder.Configuration).AddPersistence(builder.Configuration);
builder.Services.AddControllers();


var keyVaultUrl = builder.Configuration["keyVaultUrl"];
string GetEnvironmentVariable(string key) => Environment.GetEnvironmentVariable(key) ?? string.Empty;

Uri keyVaultUri = new Uri(keyVaultUrl!);
if(GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "local")
{
    string clientId = GetEnvironmentVariable("clientId");
    string tenantId = GetEnvironmentVariable("tenantId");
    string clientSecret = GetEnvironmentVariable("clientSecret");

    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    builder.Configuration.AddAzureKeyVault(keyVaultUri, credential);
}
else
    builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());

var sql = builder.Configuration["SQLConnectionString"];
var sendEmail = builder.Configuration["SendAzureEmailKey"];


// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.UseHttpsRedirection();

app.Run();