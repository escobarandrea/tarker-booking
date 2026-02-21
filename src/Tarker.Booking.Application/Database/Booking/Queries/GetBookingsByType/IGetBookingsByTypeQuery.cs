using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.Booking.Queries.GetBookingsByType
{
    public interface IGetBookingsByTypeQuery
    {
        Task<List<GetBookingsByTypeModel>> Execute(string type);
    }
}
