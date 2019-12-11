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
    public class BrandFacadeService : IBrandFacadeService
    {
        private string _apiUrl = $"{Environment.GetEnvironmentVariable("PRODUCT_REST_API_URL")}brands";

        public async Task<IEnumerable<BrandDTO>> GetBrandCollection()
        {
            HttpResponseMessage response = await _apiUrl.GetAsync();

            IEnumerable<BrandDTO> brands = await response.Content.ReadAsAsync<IEnumerable<BrandDTO>>();

            return brands;
        }
    }
}
