using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.Database.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery(IDatabaseService databaseService, IMapper mapper) : IGetAllCustomersQuery
    {
        public async Task<List<GetAllCustomersModel>> Execute()
        {
            var entities = await databaseService.Customers.ToListAsync();
            return mapper.Map<List<GetAllCustomersModel>>(entities);
        }
    }
}
