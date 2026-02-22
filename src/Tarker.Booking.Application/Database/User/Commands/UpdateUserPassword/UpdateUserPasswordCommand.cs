using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.User.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommand(IDatabaseService databaseService) : IUpdateUserPasswordCommand
    {
        public async Task<bool> ExecuteAsync(UpdateUserPasswordModel model)
        {
            var entity = await databaseService.Users.FirstOrDefaultAsync(user => user.UserId == model.UserId);

            if (entity == null)
                return false;

            entity.Password = model.Password;            

            return await databaseService.SaveAsync();
        }
    }
}
