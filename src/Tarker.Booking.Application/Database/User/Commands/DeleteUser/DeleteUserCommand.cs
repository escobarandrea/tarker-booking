using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.User.Commands.DeleteUser
{
    public class DeleteUserCommand(IDatabaseService databaseService) : IDeleteUserCommand
    {
        public async Task<bool> Execute(int userId)
        {
            var entity = await databaseService.Users.FirstOrDefaultAsync(user => user.UserId == userId);

            if (entity == null)
                return false;

            databaseService.Users.Remove(entity);
            return await databaseService.SaveAsync();
        }
    }
}
