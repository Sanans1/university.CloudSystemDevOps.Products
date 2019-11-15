using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;
using DevOps.Products.Composer.API.Services.Interfaces.Facades;

namespace DevOps.Products.Composer.API.Services.Mocks.Facades
{
    public class CustomerFacadeServiceMock : ICustomerFacadeService
    {
        private readonly IEnumerable<CustomerDTO> _mockCustomers;

        public CustomerFacadeServiceMock()
        {
            _mockCustomers = new List<CustomerDTO>
            {
                new CustomerDTO()
                {
                    ID = 1,
                    Name = "Lewis"
                },
                new CustomerDTO()
                {
                    ID = 2,
                    Name = "Sarah"
                },
                new CustomerDTO()
                {
                    ID = 3,
                    Name = "Sven"
                }
            };
        }

        public async Task<CustomerDTO> GetCustomer(int id)
        {
            return _mockCustomers.Single(customer => customer.ID == id);
        }
    }
}
