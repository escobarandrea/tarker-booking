using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.External.Email;
using Tarker.Booking.Application.Features;
using Tarker.Booking.Domain.Models.Email;

namespace Tarker.Booking.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SendEmailRequestModel model, [FromServices] ISendAzureEmailService sendAzureEmailService)
        {
            var data = await sendAzureEmailService.ExecuteAsync(model);

            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseApiService.Response(statusCode: StatusCodes.Status500InternalServerError));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK));
        }
    }
}
