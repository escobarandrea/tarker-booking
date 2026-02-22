using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.Booking.Queries.GetAllBookings
{
    public class GetAllBookingsQuery(IDatabaseService databaseService) : IGetAllBookingsQuery
    {
        public async Task<List<GetAllBookingsModel>> ExecuteAsync()
        {
            var result = await (from bookings in databaseService.Bookings
                                join customers in databaseService.Customers
                                on bookings.CustomerId equals customers.CustomerId
                                select new GetAllBookingsModel
                                {
                                    BookingId = bookings.BookingId,
                                    Code = bookings.Code,
                                    RegisterDate = bookings.RegisterDate,
                                    Type = bookings.Type,
                                    CustomerFullName = customers.FullName,
                                    CustomerDocumentNumber = customers.DocumentNumber
                                    
                                }
                                ).ToListAsync();
            return result;
        }
    }
}
