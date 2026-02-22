using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.Booking.Queries.GetBookingsByType
{
    public class GetBookingsByTypeQuery(IDatabaseService databaseService) : IGetBookingsByTypeQuery
    {
        public async Task<List<GetBookingsByTypeModel>> ExecuteAsync(string type)
        {
            var result = await (from bookings in databaseService.Bookings
                                join customers in databaseService.Customers
                                on bookings.CustomerId equals customers.CustomerId
                                where bookings.Type == type
                                select new GetBookingsByTypeModel
                                {
                                    Code = bookings.Code,
                                    RegisterDate = bookings.RegisterDate,
                                    Type = bookings.Type,
                                    CustomerFullName = customers.FullName,
                                    CustomerDocumenNumber = customers.DocumentNumber
                                }
                                ).ToListAsync();
            return result;
        }
    }
}
