using Tarker.Booking.Domain.Enums;

namespace Tarker.Booking.Application.Database.Booking.Commands.CreateBooking
{
    public class CreateBookingModel
    {
        public int BookingId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Code { get; set; }
        public BookingType Type { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
    }
}
