using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.Database.User.Commands.CreateUser;
using Tarker.Booking.Application.Database.User.Commands.DeleteUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.Database.User.Queries.GetAllUsers;
using Tarker.Booking.Application.Database.User.Queries.GetUserById;
using Tarker.Booking.Application.Database.User.Queries.GetUserByUserNameAndPassword;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model, [FromServices] ICreateUserCommand createUserCommand)
        {
            var data = await createUserCommand.Execute(model);

            return CreatedAtAction(nameof(Create), ResponseApiService.Response(statusCode: StatusCodes.Status201Created, data: data));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserModel model, [FromServices] IUpdateUserCommand updateUserCommand)
        {
            if(id <= 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            if (id != model.UserId)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest));

            var data = await updateUserCommand.Execute(model);

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpPatch("{id:int}/password")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdateUserPasswordModel model, [FromServices] IUpdateUserPasswordCommand updateUserPasswordCommand)
        {
            if (id <= 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            if (id != model.UserId)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest));

            var data = await updateUserPasswordCommand.Execute(model);

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IDeleteUserCommand deleteUserCommand)
        {
            if (id <= 0)
                return BadRequest(ResponseApiService.Response(statusCode: StatusCodes.Status400BadRequest));

            var data = await deleteUserCommand.Execute(id);

            if (!data)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IGetAllUsersQuery getAllUsersQuery)
        {
            var data = await getAllUsersQuery.Execute();

            if (data == null || data.Count == 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromServices] IGetUserByIdQuery getUserByIdQuery)
        {
            if (id <= 0)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            var data = await getUserByIdQuery.Execute(id);

            if (data == null)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByCredentials([FromQuery] string username, [FromQuery] string password, [FromServices] IGetUserByUserNameAndPasswordQuery getUserByUserNameAndPasswordQuery)
        {
            var data = await getUserByUserNameAndPasswordQuery.Execute(username, password);

            if (data == null)
                return NotFound(ResponseApiService.Response(statusCode: StatusCodes.Status404NotFound));

            return Ok(ResponseApiService.Response(statusCode: StatusCodes.Status200OK, data: data));
        }
    }
}