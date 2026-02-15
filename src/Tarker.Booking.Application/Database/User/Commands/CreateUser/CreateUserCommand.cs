using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Application.Database.User.Commands.CreateUser
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateUserCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<CreateUserModel> Execute(CreateUserModel model)
        {
            var entity = _mapper.Map<UserEntity>(model);
            await _databaseService.Users.AddAsync(entity);
            await _databaseService.SaveAsync();
            return model;
        }
    }
}
