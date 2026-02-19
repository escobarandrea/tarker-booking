using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.Customer.Queries.GetAllCustomers
{
    public interface IGetAllCustomersQuery
    {
        Task<List<GetAllCustomersModel>> Execute();
    }
}
