using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarker.Booking.Application.Database.Customer.Queries.GetCustomerByDocumentNumber
{
    public class GetCustomerByDocumentNumberQuery(IDatabaseService databaseService, IMapper mapper) : IGetCustomerByDocumentNumberQuery
    {
        public async Task<GetCustomerByDocumentNumberModel> Execute(string documentNumber)
        {
            var entity = await databaseService.Customers.FirstOrDefaultAsync(customer => customer.DocumentNumber == documentNumber);            
            return mapper.Map<GetCustomerByDocumentNumberModel>(entity);
        }
    }
}
