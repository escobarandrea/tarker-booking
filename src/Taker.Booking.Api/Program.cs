using Azure.Identity;
using Tarker.Booking.Api;
using Tarker.Booking.Application;
using Tarker.Booking.Common;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


var keyVaultUrl = builder.Configuration["keyVaultUrl"];
string GetEnvironmentVariable(string key) => Environment.GetEnvironmentVariable(key) ?? string.Empty;

Uri keyVaultUri = new Uri(keyVaultUrl!);
if (GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "local")
{
    string clientId = GetEnvironmentVariable("clientId");
    string tenantId = GetEnvironmentVariable("tenantId");
    string clientSecret = GetEnvironmentVariable("clientSecret");

    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    builder.Configuration.AddAzureKeyVault(keyVaultUri, credential);
}
else
    builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());

builder.Services.AddWebApi().AddCommon().AddApplication().AddExternal(builder.Configuration).AddPersistence(builder.Configuration);


// Add services to the container.
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();