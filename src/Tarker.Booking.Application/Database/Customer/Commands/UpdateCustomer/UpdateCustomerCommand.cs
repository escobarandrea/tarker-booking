using AutoMapper;
using Tarker.Booking.Domain.Entities.Customer;

namespace Tarker.Booking.Application.Database.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand(IDatabaseService databaseService, IMapper mapper) : IUpdateCustomerCommand
    {
        public async Task<UpdateCustomerModel> ExecuteAsync(UpdateCustomerModel model)
        {
            var entity = mapper.Map<CustomerEntity>(model);

            databaseService.Customers.Update(entity);
            await databaseService.SaveAsync();

            return model;
        }
    }
}
