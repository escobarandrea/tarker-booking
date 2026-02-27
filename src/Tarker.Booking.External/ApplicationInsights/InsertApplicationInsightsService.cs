using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Tarker.Booking.Application.External.ApplicationInsights;
using Tarker.Booking.Domain.Models.ApplicationInsights;

namespace Tarker.Booking.External.ApplicationInsights
{
    public class InsertApplicationInsightsService(IConfiguration configuration) : IInsertApplicationInsightsService
    {
        public bool Execute(InsertApplicationInsightsModel metric)
        {
            ArgumentNullException.ThrowIfNull(metric);

            var config = new TelemetryConfiguration()
            {
                ConnectionString = configuration["ApplicatinInsightsConnectionString"]
            };

            var telemetryClient = new TelemetryClient(config);
            
            var properties = new Dictionary<string, string>
            {
                { "Id", metric.Id },
                { "Content", metric.Content },
                { "Detail", metric.Detail }
            };

            telemetryClient.TrackTrace(metric.Type, SeverityLevel.Information, properties);

            return true;
        }
    }
}
