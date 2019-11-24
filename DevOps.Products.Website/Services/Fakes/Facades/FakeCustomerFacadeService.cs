using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Fakes.Facades
{
    public class FakeCustomerFacadeService : ICustomerFacadeService
    {
        private readonly IEnumerable<CustomerDTO> _mockCustomers;

        public FakeCustomerFacadeService()
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

        public async Task<CustomerDTO> GetCurrentCustomer()
        {
            return _mockCustomers.First();
        }

        public async Task<CustomerDTO> GetCustomer(int id)
        {
            return _mockCustomers.Single(customer => customer.ID == id);
        }
    }
}
