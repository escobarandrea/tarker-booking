using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Database.Booking.Commands.CreateBooking;
using Tarker.Booking.Application.Database.Booking.Queries.GetAllBookings;
using Tarker.Booking.Application.Database.Booking.Queries.GetBookingsByDocumentNumber;
using Tarker.Booking.Application.Database.Booking.Queries.GetBookingsByType;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class BookingsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingModel model, [FromServices] ICreateBookingCommand createBookingCommand, [FromServices] IValidator<CreateBookingModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if(!validate.IsValid)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            var data = await createBookingCommand.ExecuteAsync(model);

            return CreatedAtAction(nameof(Create), ResponseApiService.Response(statusCode: StatusCodes.Status201Created, data: data));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IGetAllBookingsQuery getAllBookingsQuery)
        {
            var data = await getAllBookingsQuery.ExecuteAsync();

            if (data == null || data.Count == 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? documentNumber, [FromQuery] string? type, [FromServices] IGetBookingsByDocumentNumberQuery getBookingsByDocumentNumber, [FromServices] IGetBookingsByTypeQuery getBookingsByTypeQuery)
        {
            dynamic data;

            if (string.IsNullOrWhiteSpace(documentNumber) && string.IsNullOrWhiteSpace(type))            
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, message: "At least one search parameter must be provided."));            

            if (!string.IsNullOrWhiteSpace(documentNumber))
                data = await getBookingsByDocumentNumber.ExecuteAsync(documentNumber);
            else
                data = await getBookingsByTypeQuery.ExecuteAsync(type!);
        
            if (data == null || data!.Count == 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }
    }
}
