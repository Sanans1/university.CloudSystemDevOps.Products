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
    public class ReviewFacadeService : IReviewFacadeService
    {
        private string _apiUrl = $"{Environment.GetEnvironmentVariable("REVIEW_REST_API_URL")}reviews";

        public async Task<IEnumerable<ReviewDTO>> GetReviewCollection(int id)
        {
            string url = _apiUrl.SetQueryParam("productID", id);

            HttpResponseMessage response = await url.GetAsync();

            return await response.Content.ReadAsAsync<IEnumerable<ReviewDTO>>();
        }

        public async Task<bool> CreateReview(ReviewDTO reviewDTO)
        {
            HttpResponseMessage response = await _apiUrl.PostJsonAsync(reviewDTO);

            return response.IsSuccessStatusCode;
        }

        public async Task DeleteReview(int id)
        {
            throw new NotImplementedException();
        }
    }
}
