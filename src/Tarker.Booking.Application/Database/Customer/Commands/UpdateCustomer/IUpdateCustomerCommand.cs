namespace Tarker.Booking.Application.Database.Customer.Commands.UpdateCustomer
{
    public interface IUpdateCustomerCommand
    {
        Task<UpdateCustomerModel> ExecuteAsync(UpdateCustomerModel model);
    }
}
