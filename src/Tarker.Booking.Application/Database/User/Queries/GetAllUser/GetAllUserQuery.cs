using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.User.Queries.GetAllUser
{
    public class GetAllUserQuery(IDatabaseService databaseService, IMapper mapper) : IGetAllUserQuery
    {
        public async Task<List<GetAllUserModel>> Execute()
        {
            var entities = await databaseService.Users.ToListAsync();
            return mapper.Map<List<GetAllUserModel>>(entities);
        }
    }
}
