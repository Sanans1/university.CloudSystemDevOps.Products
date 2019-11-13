using System.Threading.Tasks;
using DevOps.Products.Website.Models.ViewModels;

namespace DevOps.Products.Website.Services.Interfaces.Pages
{
    public interface IProductListService
    {
        Task<ProductListViewModel> GetProductListViewModelsAsync(string name = null, int? brandID = null, int? categoryID = null);
    }
}