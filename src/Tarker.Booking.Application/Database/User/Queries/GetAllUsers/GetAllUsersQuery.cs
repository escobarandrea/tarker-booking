using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.User.Queries.GetAllUsers
{
    public class GetAllUsersQuery(IDatabaseService databaseService, IMapper mapper) : IGetAllUsersQuery
    {
        public async Task<List<GetAllUsersModel>> Execute()
        {
            var entities = await databaseService.Users.ToListAsync();
            return mapper.Map<List<GetAllUsersModel>>(entities);
        }
    }
}
