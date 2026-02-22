namespace Tarker.Booking.Application.Database.User.Commands.CreateUser
{
    public interface ICreateUserCommand
    {
        Task<CreateUserModel> ExecuteAsync(CreateUserModel model);
    }
}
