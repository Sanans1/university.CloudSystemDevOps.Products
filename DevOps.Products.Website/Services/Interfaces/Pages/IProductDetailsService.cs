using System.Threading.Tasks;
using DevOps.Products.Website.States;
using DevOps.Products.Website.ViewModels;

namespace DevOps.Products.Website.Services.Interfaces.Pages
{
    public interface IProductDetailsService
    {
        Task<ProductDetailsState> GetProductDetailsViewModelAsync(int id);
        Task<bool> SubmitReview(ReviewViewModel review);
    }
}