namespace Tarker.Booking.Application.Database.User.Commands.UpdateUser
{
    public interface IUpdateUserCommand
    {
        Task<UpdateUserModel> ExecuteAsync(UpdateUserModel model);
    }
}
