using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomersModel
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
    }
}
