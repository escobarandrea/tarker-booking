using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tarker.Booking.Application.Database;
using Tarker.Booking.Domain.Entities.Booking;
using Tarker.Booking.Domain.Entities.Customer;
using Tarker.Booking.Domain.Entities.User;
using Tarker.Booking.Persistence.Configuration;

namespace Tarker.Booking.Persistence.Database
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        public DatabaseService(DbContextOptions options) : base(options)
        { 

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }

        public async Task<bool> SaveAsync() => await SaveChangesAsync() > 0;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }

        private void EntityConfiguration(ModelBuilder modelBuilder)
        {
            new UserConfiguration(modelBuilder.Entity<UserEntity>());
            new CustomerConfiguration(modelBuilder.Entity<CustomerEntity>());
            new BookingConfiguration(modelBuilder.Entity<BookingEntity>());
        }
    }
}
