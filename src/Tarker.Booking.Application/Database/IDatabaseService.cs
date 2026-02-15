using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tarker.Booking.Domain.Entities.Booking;
using Tarker.Booking.Domain.Entities.Customer;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Application.Database
{
    public interface IDatabaseService
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<CustomerEntity> Customers { get; set; }
        DbSet<BookingEntity> Bookings { get; set; }
        Task<bool> SaveAsync();
    }
}
