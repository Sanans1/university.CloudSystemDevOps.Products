using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class ProductListViewModel
    {
        public ProductListViewModel(IEnumerable<ProductViewModel> products, IEnumerable<CategoryViewModel> categories, IEnumerable<BrandViewModel> brands)
        {
            Products = products.ToArray();
            Categories = categories.ToArray();
            Brands = brands.ToArray();
        }

        public ProductViewModel[] Products { get; set; }
        public CategoryViewModel[] Categories { get; set; }
        public BrandViewModel[] Brands { get; set; }
    }
}
