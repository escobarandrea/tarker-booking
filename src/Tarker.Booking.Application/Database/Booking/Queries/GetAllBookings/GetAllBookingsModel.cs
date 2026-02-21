using System;
using System.Collections.Generic;
using System.Text;
using Tarker.Booking.Domain.Enums;

namespace Tarker.Booking.Application.Database.Booking.Queries.GetAllBookings
{
    public class GetAllBookingsModel
    {
        public int BookingId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerDocumentNumber { get; set; }
    }
}
