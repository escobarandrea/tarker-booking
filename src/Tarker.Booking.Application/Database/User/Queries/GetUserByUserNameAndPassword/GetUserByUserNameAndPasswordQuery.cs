using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.User.Queries.GetUserByUserNameAndPassword
{
    public class GetUserByUserNameAndPasswordQuery(IDatabaseService databaseService, IMapper mapper) : IGetUserByUserNameAndPasswordQuery
    {
        public async Task<GetUserByUserNameAndPasswordModel> Execute(string userName, string password)
        {
            var entity = await databaseService.Users.FirstOrDefaultAsync(user => EF.Functions.Like(user.UserName, userName) && user.Password == password);
            return mapper.Map<GetUserByUserNameAndPasswordModel>(entity);
        }
    }
}
