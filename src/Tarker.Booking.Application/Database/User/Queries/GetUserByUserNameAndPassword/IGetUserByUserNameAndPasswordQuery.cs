using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.User.Queries.GetUserByUserNameAndPassword
{
    public interface IGetUserByUserNameAndPasswordQuery
    {
        Task<GetUserByUserNameAndPasswordModel> Execute(string userName, string password);
    }
}
