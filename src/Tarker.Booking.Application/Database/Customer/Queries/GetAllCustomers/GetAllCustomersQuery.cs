using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
