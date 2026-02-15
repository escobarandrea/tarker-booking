using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.User.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommand(IDatabaseService databaseService) : IUpdateUserPasswordCommand
    {
        public async Task<bool> Execute(UpdateUserPasswordModel model)
        {
            var entity = await databaseService.Users.FirstOrDefaultAsync(user => user.UserId == model.UserId);

            if (entity == null)
                return false;

            entity.Password = model.Password;            

            return await databaseService.SaveAsync();
        }
    }
}
