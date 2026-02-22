namespace Tarker.Booking.Application.Database.User.Queries.GetUserByUserNameAndPassword
{
    public interface IGetUserByUserNameAndPasswordQuery
    {
        Task<GetUserByUserNameAndPasswordModel> ExecuteAsync(string userName, string password);
    }
}
