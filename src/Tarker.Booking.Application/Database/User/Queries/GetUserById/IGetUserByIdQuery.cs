namespace Tarker.Booking.Application.Database.User.Queries.GetUserById
{
    public interface IGetUserByIdQuery
    {
        Task<GetUserByIdModel> ExecuteAsync(int userId);
    }
}
