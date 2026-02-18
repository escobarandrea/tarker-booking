using Tarker.Booking.Api;
using Tarker.Booking.Application;
using Tarker.Booking.Common;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApi().AddCommon().AddApplication().AddExternal(builder.Configuration).AddPersistence(builder.Configuration);

// Add services to the container.

var app = builder.Build();

//// Test endpoints
//var users = app.MapGroup("/test/users");

//users.MapPost("/", async (CreateUserModel model, ICreateUserCommand service) =>
//{
//    var result = await service.Execute(model);
//    return Results.Ok(result);
//});

//users.MapPut("/{userId:int}", async (int userId, UpdateUserModel model, IUpdateUserCommand service) =>
//{
//    var updated = await service.Execute(model);

//    if (updated == null)
//        return Results.NotFound($"User with id {userId} was not found.");

//    return Results.NoContent();
//});

//users.MapDelete("/{userId:int}", async (int userId, IDeleteUserCommand service) =>
//{
//    var deleted = await service.Execute(userId);

//    if (!deleted)
//        return Results.NotFound($"User with id {userId} was not found.");

//    return Results.NoContent();
//});

//users.MapPatch("/{userId:int}/password", async (int userId, UpdateUserPasswordModel model, IUpdateUserPasswordCommand service) =>
//{
//    var updated = await service.Execute(model);

//    if (updated == false)
//        return Results.NotFound($"User with id {userId} was not found.");

//    return Results.NoContent();
//});

//users.MapGet("/", async (IGetAllUserQuery service) =>
//{
//    return Results.Ok(service.Execute());
//});

//users.MapGet("/{userId:int}/", async (int userId, IGetUserByIdQuery service) =>
//{
//    return Results.Ok(service.Execute(userId));
//});

//users.MapGet("/search", async (string username, string password, IGetUserByUserNameAndPasswordQuery service) =>
//{
//    return Results.Ok(service.Execute(username, password));
//});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();