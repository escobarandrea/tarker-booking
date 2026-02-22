namespace Tarker.Booking.Application.Database.Customer.Commands.CreateCustomer
{
    public interface ICreateCustomerCommand
    {
        Task<CreateCustomerModel> ExecuteAsync(CreateCustomerModel model);
    }
}
