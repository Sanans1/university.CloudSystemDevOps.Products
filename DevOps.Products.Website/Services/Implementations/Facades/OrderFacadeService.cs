using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces.Facades;
using Flurl;
using Flurl.Http;

namespace DevOps.Products.Website.Services.Implementations.Facades
{
    public class OrderFacadeService : IOrderFacadeService
    {
        private string _apiUrl = $"{Environment.GetEnvironmentVariable("ORDER_REST_API_URL")}orders";

        public async Task<IEnumerable<OrderDTO>> GetOrders(string customerUsername, int productID)
        {
            string url = _apiUrl.SetQueryParam("customerUsername", customerUsername);
            url = url.SetQueryParam("productID", productID);

            HttpResponseMessage response = await url.GetAsync();

            IEnumerable<OrderDTO> orders = await response.Content.ReadAsAsync<IEnumerable<OrderDTO>>();

            return orders;
        }

        public async Task<bool> CreateOrder(OrderDTO order)
        {
            HttpResponseMessage response = await _apiUrl.PostJsonAsync(order);

            return response.IsSuccessStatusCode;
        }
    }
}
