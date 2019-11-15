using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Models.ViewModels.ProductList;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;
using DevOps.Products.Website.Services.Interfaces.Pages;
using DevOps.Products.Website.States;

namespace DevOps.Products.Website.Services.Implementations.Pages
{
    public class ProductListService : IProductListService
    {
        private readonly IMapper _mapper;
        private readonly IProductFacadeService _productFacadeService;
        private readonly ICategoryFacadeService _categoryFacadeService;
        private readonly IBrandFacadeService _brandFacadeService;

        public ProductListService(IMapper mapper, IProductFacadeService productFacadeService, ICategoryFacadeService categoryFacadeService, IBrandFacadeService brandFacadeService)
        {
            _mapper = mapper;
            _productFacadeService = productFacadeService;
            _categoryFacadeService = categoryFacadeService;
            _brandFacadeService = brandFacadeService;
        }

        public async Task<ProductListState> GetProductListViewModelsAsync(string name = null, int? categoryID = null, int? brandID = null)
        {
            IEnumerable<ProductViewModel> products = _mapper.Map<IEnumerable<ProductViewModel>>(await _productFacadeService.GetProductCollection(name, categoryID, brandID));
            IEnumerable<CategoryViewModel> categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryFacadeService.GetCategoryCollection());
            IEnumerable<BrandViewModel> brands = _mapper.Map<IEnumerable<BrandViewModel>>(await _brandFacadeService.GetBrandCollection());

            return new ProductListState(products, categories, brands);
        }
    }
}
