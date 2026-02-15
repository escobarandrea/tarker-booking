using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Application.Database.User.Commands.CreateUser
{
    public class CreateUserCommand(IDatabaseService databaseService, IMapper mapper) : ICreateUserCommand
    {
        public async Task<CreateUserModel> Execute(CreateUserModel model)
        {
            var entity = mapper.Map<UserEntity>(model);
            await databaseService.Users.AddAsync(entity);
            await databaseService.SaveAsync();
            return model;
        }
    }
}
