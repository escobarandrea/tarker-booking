using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Database.User.Commands.CreateUser;
using Tarker.Booking.Application.Database.User.Commands.DeleteUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.Database.User.Queries.GetAllUsers;
using Tarker.Booking.Application.Database.User.Queries.GetUserById;
using Tarker.Booking.Application.Database.User.Queries.GetUserByUserNameAndPassword;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.External.ApplicationInsights;
using Tarker.Booking.Application.External.GetTokenJwt;
using Tarker.Booking.Application.Features;
using Tarker.Booking.Common.Constants;
using Tarker.Booking.Domain.Models.ApplicationInsights;

namespace Tarker.Booking.Api.Controllers
{
#pragma warning disable
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UsersController( IInsertApplicationInsightsService insertApplicationInsightsService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model, [FromServices] ICreateUserCommand createUserCommand, [FromServices] IValidator<CreateUserModel> validator)
        {
            var validate = await validator.ValidateAsync(model);
            
            if(!validate.IsValid)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            var data = await createUserCommand.ExecuteAsync(model);

            return CreatedAtAction(nameof(Create), ResponseApiService.Response(statusCode: StatusCodes.Status201Created, data: data));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserModel model, [FromServices] IUpdateUserCommand updateUserCommand, [FromServices] IValidator<UpdateUserModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            if (id <= 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            if (id != model.UserId)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest));

            var data = await updateUserCommand.ExecuteAsync(model);

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpPatch("{id:int}/password")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdateUserPasswordModel model, [FromServices] IUpdateUserPasswordCommand updateUserPasswordCommand, [FromServices] IValidator<UpdateUserPasswordModel> validator)
        {
            var validate = await validator.ValidateAsync(model);

            if (!validate.IsValid)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            if (id <= 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            if (id != model.UserId)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest));

            var data = await updateUserPasswordCommand.ExecuteAsync(model);

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IDeleteUserCommand deleteUserCommand)
        {
            if (id <= 0)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest));

            var data = await deleteUserCommand.ExecuteAsync(id);

            if (!data)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IGetAllUsersQuery getAllUsersQuery)
        {
            var metric = new InsertApplicationInsightsModel(ApplicationInsightsConstants.MetricTypeApiCall, EntitiesConstants.User, nameof(GetAll));
            insertApplicationInsightsService.Execute(metric);

            var data = await getAllUsersQuery.ExecuteAsync();

            if (data == null || data.Count == 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromServices] IGetUserByIdQuery getUserByIdQuery)
        {
            var metric = new InsertApplicationInsightsModel(ApplicationInsightsConstants.MetricTypeApiCall, EntitiesConstants.User, nameof(GetById));
            insertApplicationInsightsService.Execute(metric);

            if (id <= 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            var data = await getUserByIdQuery.ExecuteAsync(id);

            if (data == null)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password, 
            [FromServices] IGetUserByUserNameAndPasswordQuery getUserByUserNameAndPasswordQuery, 
            [FromServices] IValidator<(string UserName, string Password)> validator, 
            [FromServices] IGetTokenJwtService getTokenJwtService)
        {
            var metric = new InsertApplicationInsightsModel(ApplicationInsightsConstants.MetricTypeApiCall, EntitiesConstants.User, nameof(Login));
            insertApplicationInsightsService.Execute(metric);

            var validate = await validator.ValidateAsync((username, password));

            if (!validate.IsValid)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest, data: validate.Errors));

            var data = await getUserByUserNameAndPasswordQuery.ExecuteAsync(username, password);

            if (data == null)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            data.Token = getTokenJwtService.Execute(data.UserId.ToString());

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }
    }
}