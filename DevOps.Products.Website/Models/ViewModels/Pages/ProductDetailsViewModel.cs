using System.Collections.Generic;
using System.Linq;

namespace DevOps.Products.Website.Models.ViewModels.Pages
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel(ProductViewModel product, IEnumerable<ReviewViewModel> reviews, CustomerViewModel customer)
        {
            Product = product;
            Reviews = reviews;
            Customer = customer;
        }

        public ProductViewModel Product { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
