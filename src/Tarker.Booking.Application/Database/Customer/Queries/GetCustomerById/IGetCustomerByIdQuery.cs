namespace Tarker.Booking.Application.Database.Customer.Queries.GetCustomerById
{
    public interface IGetCustomerByIdQuery
    {
        Task<GetCustomerByIdModel> ExecuteAsync(int customerId);
    }
}
