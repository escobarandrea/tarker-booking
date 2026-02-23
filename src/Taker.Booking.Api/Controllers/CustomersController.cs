using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Database.Customer.Commands.CreateCustomer;
using Tarker.Booking.Application.Database.Customer.Commands.DeleteCustomer;
using Tarker.Booking.Application.Database.Customer.Commands.UpdateCustomer;
using Tarker.Booking.Application.Database.Customer.Queries.GetAllCustomers;
using Tarker.Booking.Application.Database.Customer.Queries.GetCustomerByDocumentNumber;
using Tarker.Booking.Application.Database.Customer.Queries.GetCustomerById;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class CustomersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerModel model, [FromServices] ICreateCustomerCommand createCustomerCommand, [FromServices] IValidator<CreateCustomerModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if(!validate.IsValid)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            var data = await createCustomerCommand.ExecuteAsync(model);

            return CreatedAtAction(nameof(Create), ResponseApiService.Response(statusCode: StatusCodes.Status201Created, data: data));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerModel model, [FromServices] IUpdateCustomerCommand updateCustomerCommand, [FromServices] IValidator<UpdateCustomerModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            if (id <= 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            if (id != model.CustomerId)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest));

            var data = await updateCustomerCommand.ExecuteAsync(model);

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IDeleteCustomerCommand deleteCustomerCommand)
        {
            if (id <= 0)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest));

            var data = await deleteCustomerCommand.ExecuteAsync(id);

            if (!data)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IGetAllCustomersQuery getAllCustomersQuery)
        {
            var data = await getAllCustomersQuery.ExecuteAsync();

            if (data == null || data.Count == 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromServices] IGetCustomerByIdQuery getCustomerByIdQuery)
        {
            if (id <= 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            var data = await getCustomerByIdQuery.ExecuteAsync(id);

            if (data == null)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByDocumentNumber([FromQuery] string documentNumber, [FromServices] IGetCustomerByDocumentNumberQuery getCustomerByDocumentNumberQuery)
        {
            var data = await getCustomerByDocumentNumberQuery.ExecuteAsync(documentNumber);

            if(data == null)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }
    }
}
