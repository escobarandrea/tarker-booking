using Microsoft.EntityFrameworkCore;
using Tarker.Booking.Api;
using Tarker.Booking.Application;
using Tarker.Booking.Common;
using Tarker.Booking.Domain.Entities.User;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;
using Tarker.Booking.Persistence.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApi().AddCommon().AddApplication().AddExternal(builder.Configuration).AddPersistence(builder.Configuration);

// Add services to the container.

var app = builder.Build();

//app.MapPost("/test/add", async (IDatabaseService databaseService) =>
//{
//    var user = new UserEntity
//    {
//        FirstName = "Andrea",
//        LastName = "Escobar",
//        UserName = "andreaescobar",
//        Password = "a9F3c7B1xZ"
//    };
//    await databaseService.Users.AddAsync(user);
//    await databaseService.SaveAsync();

//    return "Create OK";
//});
//app.MapGet("/test/getAll", async (IDatabaseService databaseService) =>
//{
//    var users = await databaseService.Users.ToListAsync();
//    return users;
//});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();