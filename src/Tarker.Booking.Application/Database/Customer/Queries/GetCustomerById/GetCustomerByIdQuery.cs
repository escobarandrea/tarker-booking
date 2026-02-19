using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.Customer.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery(IDatabaseService databaseService, IMapper mapper) : IGetCustomerByIdQuery
    {
        public async Task<GetCustomerByIdModel> Execute(int customerId)
        {
            var entity = databaseService.Customers.FirstOrDefaultAsync(customer => customer.CustomerId == customerId);
            return mapper.Map<GetCustomerByIdModel>(entity);
        }
    }
}
