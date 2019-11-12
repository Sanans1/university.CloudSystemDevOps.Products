using System.Threading.Tasks;
using DevOps.Products.Website.Models.ViewModels;

namespace DevOps.Products.Website.Services.Interfaces
{
    public interface IProductDetailsService
    {
        Task<ProductDetailsViewModel> GetProductDetailsViewModelAsync(int id);
        Task<bool> SubmitReview(ProductDetailsViewModel productDetailsViewModel, string reviewText, int rating);
    }
}