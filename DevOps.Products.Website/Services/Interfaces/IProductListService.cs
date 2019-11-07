using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.ViewModels;

namespace DevOps.Products.Website.Services.Interfaces
{
    public interface IProductListService
    {
        Task<ProductListViewModel[]> GetProductListViewModelsAsync();
    }
}