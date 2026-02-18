using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Tarker.Booking.Application.Database.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand(IDatabaseService databaseService) : IDeleteCustomerCommand
    {
        public async Task<bool> Execute(int customerId)
        {
            var entity = await databaseService.Customers.FirstOrDefaultAsync(customer => customer.CustomerId == customerId);
            
            if (entity == null)
                return false;

            databaseService.Customers.Remove(entity);
            return await databaseService.SaveAsync();
        }
    }
}
