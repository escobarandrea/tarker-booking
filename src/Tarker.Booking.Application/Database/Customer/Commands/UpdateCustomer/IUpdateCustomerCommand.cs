using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.Customer.Commands.UpdateCustomer
{
    public interface IUpdateCustomerCommand
    {
        Task<UpdateCustomerModel> Execute(UpdateCustomerModel model);
    }
}
