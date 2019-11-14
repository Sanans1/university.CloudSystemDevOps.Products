using System.Collections.Generic;
using DevOps.Products.Website.ViewModels;
using DevOps.Products.Website.ViewModels.ProductList;

namespace DevOps.Products.Website.States
{
    public class ProductListState
    {
        public ProductListState(IEnumerable<ProductViewModel> products, IEnumerable<CategoryViewModel> categories, IEnumerable<BrandViewModel> brands)
        {
            Products = products;
            Categories = categories;
            Brands = brands;
        }

        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<BrandViewModel> Brands { get; set; }
    }
}
