using System.Threading.Tasks;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Models.ViewModels.Pages;

namespace DevOps.Products.Website.Services.Interfaces.Pages
{
    public interface IProductDetailsService
    {
        Task<ProductDetailsViewModel> GetProductDetailsViewModelAsync(int id);
        Task<bool> SubmitReview(ReviewViewModel review);
    }
}