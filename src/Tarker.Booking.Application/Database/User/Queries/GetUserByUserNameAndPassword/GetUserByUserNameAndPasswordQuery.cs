using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.User.Queries.GetUserByUserNameAndPassword
{
    public class GetUserByUserNameAndPasswordQuery(IDatabaseService databaseService, IMapper mapper) : IGetUserByUserNameAndPasswordQuery
    {
        public async Task<GetUserByUserNameAndPasswordModel> Execute(string userName, string password)
        {
            var entity = await databaseService.Users.FirstOrDefaultAsync(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            return mapper.Map<GetUserByUserNameAndPasswordModel>(entity);
        }
    }
}
