using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.User.Commands.CreateUser
{
    public interface ICreateUserCommand
    {
        Task<CreateUserModel> Execute(CreateUserModel model);
    }
}
