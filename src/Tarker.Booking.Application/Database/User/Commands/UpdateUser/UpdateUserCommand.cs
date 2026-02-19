using AutoMapper;
using Tarker.Booking.Domain.Entities.User;

namespace Tarker.Booking.Application.Database.User.Commands.UpdateUser
{
    public class UpdateUserCommand(IDatabaseService databaseService, IMapper mapper) : IUpdateUserCommand
    {
        public async Task<UpdateUserModel> Execute(UpdateUserModel model)
        {
            var entity = mapper.Map<UserEntity>(model);
            databaseService.Users.Update(entity);
            await databaseService.SaveAsync();
            return model;
        }
    }
}
