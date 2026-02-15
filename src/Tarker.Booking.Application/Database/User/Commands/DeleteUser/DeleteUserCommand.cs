using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly IDatabaseService _databaseService;

        public DeleteUserCommand(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> Execute(int userId)
        {
            var entity = await _databaseService.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (entity == null)
                return false;

            _databaseService.Users.Remove(entity);
            return await _databaseService.SaveAsync();
        }
    }
}
