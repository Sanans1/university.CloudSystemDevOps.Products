using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.DTOs;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Services.Interfaces;
using DevOps.Products.Website.Services.Interfaces.Facades;
using DevOps.Products.Website.Services.Interfaces.Pages;

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

        public async Task<ProductListViewModel> GetProductListViewModelsAsync(string name = null, int? brandID = null, int? categoryID = null)
        {
            IEnumerable<ProductViewModel> products = _mapper.Map<IEnumerable<ProductViewModel>>(await _productFacadeService.GetProductCollection(name, brandID, categoryID));
            IEnumerable<CategoryViewModel> categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryFacadeService.GetCategoryCollection());
            IEnumerable<BrandViewModel> brands = _mapper.Map<IEnumerable<BrandViewModel>>(await _brandFacadeService.GetBrandCollection());

            return new ProductListViewModel(products, categories, brands);
        }
    }
}
