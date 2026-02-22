using AutoMapper;
using Tarker.Booking.Domain.Entities.Customer;

namespace Tarker.Booking.Application.Database.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand(IDatabaseService databaseService, IMapper mapper) : ICreateCustomerCommand
    {
        public async Task<CreateCustomerModel> ExecuteAsync(CreateCustomerModel model)
        {
            var entity = mapper.Map<CustomerEntity>(model);
            await databaseService.Customers.AddAsync(entity);
            await databaseService.SaveAsync();
            return model;
        }
    }
}
