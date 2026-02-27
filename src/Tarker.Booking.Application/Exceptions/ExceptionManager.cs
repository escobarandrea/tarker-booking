using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tarker.Booking.Application.External.ApplicationInsights;
using Tarker.Booking.Application.Features;
using Tarker.Booking.Common.Constants;
using Tarker.Booking.Domain.Models.ApplicationInsights;

namespace Tarker.Booking.Application.Exceptions
{
    public class ExceptionManager(IInsertApplicationInsightsService insertApplicationInsightsService) : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(ResponseApiService.Response(statusCode: StatusCodes.Status500InternalServerError, data: null, message: context.Exception.Message));

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var metric = new InsertApplicationInsightsModel(ApplicationInsightsConstants.MetricTypeError, context.Exception.Message, context.Exception.ToString());
            insertApplicationInsightsService.Execute(metric);
        }
    }
}
