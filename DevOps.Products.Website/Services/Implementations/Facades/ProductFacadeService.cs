using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;
using Flurl;
using Flurl.Http;

namespace DevOps.Products.Website.Services.Implementations.Facades
{
    public class ProductFacadeService : IProductFacadeService
    {
        private const string API_URL = "http://localhost:62991/api/Products";

        public async Task<IEnumerable<ProductDTO>> GetProductCollection(string searchString = null, int? categoryID = null, int? brandID = null)
        {
            string url = API_URL.SetQueryParam("searchString", searchString);
            url = url.SetQueryParam("categoryID", categoryID);
            url = url.SetQueryParam("brandID", brandID);

            HttpResponseMessage response = await url.GetAsync();

            IEnumerable<ProductDTO> products = await response.Content.ReadAsAsync<IEnumerable<ProductDTO>>();

            return products;
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            string url = API_URL.AppendPathSegment(id);

            HttpResponseMessage response = await url.GetAsync();

            ProductDTO product = await response.Content.ReadAsAsync<ProductDTO>();

            return product;
        }
    }
}
