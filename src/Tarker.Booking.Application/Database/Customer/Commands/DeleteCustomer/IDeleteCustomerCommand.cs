namespace Tarker.Booking.Application.Database.Customer.Commands.DeleteCustomer
{
    public interface IDeleteCustomerCommand
    {
        Task<bool> ExecuteAsync(int customerId);
    }
}
