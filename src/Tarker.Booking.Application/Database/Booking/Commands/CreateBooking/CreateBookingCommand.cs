using AutoMapper;
using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Application.Database.Booking.Commands.CreateBooking
{
    public class CreateBookingCommand(IDatabaseService databaseService, IMapper mapper) : ICreateBookingCommand
    {
        public async Task<CreateBookingModel> Execute(CreateBookingModel model)
        {
            var entity = mapper.Map<BookingEntity>(model);

            await databaseService.Bookings.AddAsync(entity);
            await databaseService.SaveAsync();
            return model;
        }
    }
}
