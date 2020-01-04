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
    public class CustomerFacadeService : ICustomerFacadeService
    {
        private string _apiUrl = $"{Environment.GetEnvironmentVariable("CUSTOMER_REST_API_URL")}customers";

        public async Task<CustomerDTO> GetCustomer(string customerUsername)
        {
            string url = _apiUrl.SetQueryParam("username", customerUsername);

            HttpResponseMessage response = await url.GetAsync();

            CustomerDTO customer = await response.Content.ReadAsAsync<CustomerDTO>();

            return customer;
        }
    }
}
