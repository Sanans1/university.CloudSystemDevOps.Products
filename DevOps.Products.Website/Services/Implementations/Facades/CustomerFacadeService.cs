using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Implementations.Facades
{
    public class CustomerFacadeService : ICustomerFacadeService
    {
        public async Task<CustomerDTO> GetCurrentCustomer()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDTO> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
