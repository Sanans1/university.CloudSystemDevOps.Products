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
                    Username = "Lewis",
                    DeliveryAddress = "FakeTown, FakeRoad, 1",
                    TelephoneNumber = "12345678"
                },
                new CustomerDTO()
                {
                    ID = 2,
                    Username = "Bill",
                    DeliveryAddress = "FakeTown, FakeRoad, 2",
                    TelephoneNumber = "12345678"
                },
                new CustomerDTO()
                {
                    ID = 3,
                    Username = "Bob",
                    DeliveryAddress = "FakeTown, FakeRoad, 3",
                    TelephoneNumber = "12345678"
                }
            };
        }

        public async Task<CustomerDTO> GetCustomer(string customerUsername)
        {
            return _mockCustomers.Single(customer => customer.Username == customerUsername);
        }
    }
}
