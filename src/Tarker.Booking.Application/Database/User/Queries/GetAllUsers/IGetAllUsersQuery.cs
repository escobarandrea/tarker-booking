using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.User.Queries.GetAllUsers
{
    public interface IGetAllUsersQuery
    {
        Task<List<GetAllUsersModel>> Execute();
    }
}
