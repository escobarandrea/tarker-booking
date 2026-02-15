using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.User.Queries.GetAllUser
{
    public interface IGetAllUserQuery
    {
        Task<List<GetAllUserModel>> Execute();
    }
}
