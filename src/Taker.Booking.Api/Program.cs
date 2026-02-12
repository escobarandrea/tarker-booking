using Microsoft.EntityFrameworkCore;
using Tarker.Booking.Application.Interfaces;
using Tarker.Booking.Domain.Entities.User;
using Tarker.Booking.Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseService>(options => options.UseSqlServer(builder.Configuration["SQLConnectionString"]));
builder.Services.AddScoped<IDatabaseService, DatabaseService>();

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