namespace Tarker.Booking.Application.Database.Booking.Queries.GetBookingsByDocumentNumber
{
    public interface IGetBookingsByDocumentNumberQuery
    {
        Task<List<GetBookingsByDocumentNumberModel>> ExecuteAsync(string documentNumber);
    }
}
