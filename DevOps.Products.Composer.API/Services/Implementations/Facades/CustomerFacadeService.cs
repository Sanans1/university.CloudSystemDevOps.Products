using System;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;

namespace DevOps.Products.Composer.API.Services.Implementations.Facades
{
    public class CustomerFacadeService : ICustomerFacadeService
    {
        public async Task<CustomerDTO> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
