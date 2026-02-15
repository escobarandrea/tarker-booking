using Tarker.Booking.Api;
using Tarker.Booking.Application;
using Tarker.Booking.Common;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApi().AddCommon().AddApplication().AddExternal(builder.Configuration).AddPersistence(builder.Configuration);

// Add services to the container.

var app = builder.Build();

// Test endpoints
//app.MapPost("/test/users", async (CreateUserModel model, ICreateUserCommand service) =>
//{
//    var result = await service.Execute(model);
//    return Results.Ok(result);
//});

//app.MapPut("/test/users/{userId:int}", async (int userId, UpdateUserModel model, IUpdateUserCommand service) =>
//{
//    var updated = await service.Execute(model);

//    if (updated == null)
//        return Results.NotFound($"User with id {userId} was not found.");

//    return Results.NoContent();
//});

//app.MapDelete("test/users/{userId:int}", async (int userId, IDeleteUserCommand service) =>
//{
//    var deleted = await service.Execute(userId);

//    if (!deleted)
//        return Results.NotFound($"User with id {userId} was not found.");

//    return Results.NoContent();
//});

//app.MapPatch("/test/users/{userId:int}/password", async (int userId, UpdateUserPasswordModel model, IUpdateUserPasswordCommand service) =>
//{
//    var updated = await service.Execute(model);

//    if (updated == false)
//        return Results.NotFound($"User with id {userId} was not found.");

//    return Results.NoContent();
//});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();