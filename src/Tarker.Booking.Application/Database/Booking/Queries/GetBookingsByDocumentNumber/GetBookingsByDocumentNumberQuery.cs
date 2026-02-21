using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.Booking.Queries.GetBookingsByDocumentNumber
{
    public class GetBookingsByDocumentNumberQuery(IDatabaseService databaseService) : IGetBookingsByDocumentNumberQuery
    {
        public async Task<List<GetBookingsByDocumentNumberModel>> Execute(string documentNumber)
        {
            var result = await (from bookings in databaseService.Bookings
                                join customers in databaseService.Customers
                                on bookings.CustomerId equals customers.CustomerId
                                where customers.DocumentNumber == documentNumber
                                select new GetBookingsByDocumentNumberModel
                                {
                                    Code = bookings.Code,
                                    RegisterDate = bookings.RegisterDate,
                                    Type = bookings.Type.ToString()
                                }
                                ).ToListAsync();
            return result;
        }
    }
}
