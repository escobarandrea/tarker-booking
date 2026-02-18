using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tarker.Booking.Domain.Entities.Customer;

namespace Tarker.Booking.Application.Database.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand(IDatabaseService databaseService, IMapper mapper) : IUpdateCustomerCommand
    {
        public async Task<UpdateCustomerModel> Execute(UpdateCustomerModel model)
        {
            var entity = mapper.Map<CustomerEntity>(model);

            databaseService.Customers.Update(entity);
            await databaseService.SaveAsync();

            return model;
        }
    }
}
