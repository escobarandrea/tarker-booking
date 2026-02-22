using AutoMapper;
using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Application.Database.Booking.Commands.CreateBooking
{
    public class CreateBookingCommand(IDatabaseService databaseService, IMapper mapper) : ICreateBookingCommand
    {
        public async Task<CreateBookingModel> ExecuteAsync(CreateBookingModel model)
        {
            var entity = mapper.Map<BookingEntity>(model);
            entity.RegisterDate = DateTime.Now;

            await databaseService.Bookings.AddAsync(entity);
            await databaseService.SaveAsync();
            return model;
        }
    }
}
