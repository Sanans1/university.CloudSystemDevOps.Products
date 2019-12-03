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
        private const string API_URL = "http://products/api/Brands";

        public async Task<IEnumerable<BrandDTO>> GetBrandCollection()
        {
            HttpResponseMessage response = await API_URL.GetAsync();

            IEnumerable<BrandDTO> brands = await response.Content.ReadAsAsync<IEnumerable<BrandDTO>>();

            return brands;
        }
    }
}
