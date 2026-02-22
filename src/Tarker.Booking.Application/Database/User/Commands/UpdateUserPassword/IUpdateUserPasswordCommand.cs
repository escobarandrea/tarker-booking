namespace Tarker.Booking.Application.Database.User.Commands.UpdateUserPassword
{
    public interface IUpdateUserPasswordCommand
    {
        Task<bool> ExecuteAsync(UpdateUserPasswordModel model);
    }
}
