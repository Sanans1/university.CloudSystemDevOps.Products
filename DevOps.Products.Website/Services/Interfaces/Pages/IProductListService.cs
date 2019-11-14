using System.Threading.Tasks;
using DevOps.Products.Website.States;

namespace DevOps.Products.Website.Services.Interfaces.Pages
{
    public interface IProductListService
    {
        Task<ProductListState> GetProductListViewModelsAsync(string name = null, int? categoryID = null, int? brandID = null);
    }
}