namespace Tarker.Booking.Application.Database.User.Queries.GetAllUsers
{
    public interface IGetAllUsersQuery
    {
        Task<List<GetAllUsersModel>> ExecuteAsync();
    }
}
