using Tarker.Booking.Api;
using Tarker.Booking.Application;
using Tarker.Booking.Application.Database.Booking.Commands.CreateBooking;
using Tarker.Booking.Application.Database.Booking.Queries.GetAllBookings;
using Tarker.Booking.Application.Database.Booking.Queries.GetBookingsByDocumentNumber;
using Tarker.Booking.Application.Database.Booking.Queries.GetBookingsByType;
using Tarker.Booking.Application.Database.Customer.Commands.CreateCustomer;
using Tarker.Booking.Application.Database.Customer.Commands.DeleteCustomer;
using Tarker.Booking.Application.Database.Customer.Commands.UpdateCustomer;
using Tarker.Booking.Application.Database.Customer.Queries.GetAllCustomers;
using Tarker.Booking.Application.Database.Customer.Queries.GetCustomerByDocumentNumber;
using Tarker.Booking.Application.Database.Customer.Queries.GetCustomerById;
using Tarker.Booking.Application.Database.User.Commands.CreateUser;
using Tarker.Booking.Application.Database.User.Commands.DeleteUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUser;
using Tarker.Booking.Application.Database.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.Database.User.Queries.GetAllUsers;
using Tarker.Booking.Application.Database.User.Queries.GetUserById;
using Tarker.Booking.Application.Database.User.Queries.GetUserByUserNameAndPassword;
using Tarker.Booking.Common;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApi().AddCommon().AddApplication().AddExternal(builder.Configuration).AddPersistence(builder.Configuration);

// Add services to the container.

var app = builder.Build();

//// Test endpoints
var users = app.MapGroup("/test/users");

users.MapPost("/", async (CreateUserModel model, ICreateUserCommand service) =>
{
    var result = await service.Execute(model);
    return Results.Ok(result);
});

users.MapPut("/{userId:int}", async (int userId, UpdateUserModel model, IUpdateUserCommand service) =>
{
    var updated = await service.Execute(model);

    if (updated == null)
        return Results.NotFound($"User with id {userId} was not found.");

    return Results.NoContent();
});

users.MapDelete("/{userId:int}", async (int userId, IDeleteUserCommand service) =>
{
    var deleted = await service.Execute(userId);

    if (!deleted)
        return Results.NotFound($"User with id {userId} was not found.");

    return Results.NoContent();
});

users.MapPatch("/{userId:int}/password", async (int userId, UpdateUserPasswordModel model, IUpdateUserPasswordCommand service) =>
{
    var updated = await service.Execute(model);

    if (updated == false)
        return Results.NotFound($"User with id {userId} was not found.");

    return Results.NoContent();
});

users.MapGet("/", async (IGetAllUsersQuery service) =>
{
    return Results.Ok(await service.Execute());
});

users.MapGet("/{userId:int}/", async (int userId, IGetUserByIdQuery service) =>
{
    return Results.Ok(await service.Execute(userId));
});

users.MapGet("/search", async (string username, string password, IGetUserByUserNameAndPasswordQuery service) =>
{
    return Results.Ok(await service.Execute(username, password));
});

var customers = app.MapGroup("/test/customers");

customers.MapPost("/", async (CreateCustomerModel model, ICreateCustomerCommand service) =>
{
    var result = await service.Execute(model);
    return Results.Ok(result);
});

customers.MapPut("/{customerId:int}", async (int customerId, UpdateCustomerModel model, IUpdateCustomerCommand service) =>
{
    var updated = await service.Execute(model);

    if (updated == null)
        return Results.NotFound($"User with id {customerId} was not found.");

    return Results.NoContent();
});

customers.MapDelete("/{customerId:int}", async (int customerId, IDeleteCustomerCommand service) =>
{
    var deleted = await service.Execute(customerId);

    if (!deleted)
        return Results.NotFound($"User with id {customerId} was not found.");

    return Results.NoContent();
});

customers.MapGet("/", async (IGetAllCustomersQuery service) =>
{
    return Results.Ok(await service.Execute());
});

customers.MapGet("/{customerId:int}/", async (int customerId, IGetCustomerByIdQuery service) =>
{
    return Results.Ok(await service.Execute(customerId));
});

customers.MapGet("/search", async (string documentNumber, IGetCustomerByDocumentNumberQuery service) =>
{
    return Results.Ok(await service.Execute(documentNumber));
});

var bookings = app.MapGroup("/test/bookings");

bookings.MapPost("/", async (CreateBookingModel model, ICreateBookingCommand service) =>
{
    var result = await service.Execute(model);
    return Results.Ok(result);
});

bookings.MapGet("/", async (IGetAllBookingsQuery service) =>
{
    return Results.Ok(await service.Execute());
});

bookings.MapGet("/search/by-document", async (string documentNumber, IGetBookingsByDocumentNumberQuery service) =>
{
    return Results.Ok(await service.Execute(documentNumber));
});

bookings.MapGet("/search/by-type", async (string type, IGetBookingsByTypeQuery service) =>
{
    return Results.Ok(await service.Execute(type));
});


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();