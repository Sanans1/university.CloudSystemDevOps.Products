using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces.Facades;

namespace DevOps.Products.Website.Services.Fakes.Facades
{
    public class FakeOrderFacadeService : IOrderFacadeService
    {

        #region fields

        private readonly IEnumerable<OrderDTO> _mockOrders;

        #endregion

        public FakeOrderFacadeService()
        {
            _mockOrders = new List<OrderDTO>
            {
                new OrderDTO
                {
                    ID = 1,
                    CustomerUsername = "Bill",
                    ProductID = 1
                },
                new OrderDTO
                {
                    ID = 2,
                    CustomerUsername = "Bob",
                    ProductID = 1
                },
                new OrderDTO
                {
                    ID = 3,
                    CustomerUsername = "Bill",
                    ProductID = 2
                },
            };
        }

        #region methods

        public async Task<IEnumerable<OrderDTO>> GetOrders(string customerUsername, int productID)
        {
            return _mockOrders.Where(product =>
                product.CustomerUsername == customerUsername && product.ProductID == productID);
        }

        #endregion
    }
}
