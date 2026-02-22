using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.User.Queries.GetUserById
{
    public class GetUserByIdQuery(IDatabaseService databaseService, IMapper mapper) : IGetUserByIdQuery
    {
        public async Task<GetUserByIdModel> ExecuteAsync(int userId)
        {
            var entity = await databaseService.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            return mapper.Map<GetUserByIdModel>(entity);
        }
    }
}
