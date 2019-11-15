using System.Threading.Tasks;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.States;

namespace DevOps.Products.Website.Services.Interfaces.Pages
{
    public interface IProductDetailsService
    {
        Task<ProductDetailsState> GetProductDetailsViewModelAsync(int id);
        Task<bool> SubmitReview(ReviewViewModel review);
    }
}