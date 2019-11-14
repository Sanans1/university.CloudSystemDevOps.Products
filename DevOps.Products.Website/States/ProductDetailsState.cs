using System.Collections.Generic;
using DevOps.Products.Website.ViewModels;

namespace DevOps.Products.Website.States
{
    public class ProductDetailsState
    {
        public ProductDetailsState(ProductDetailsViewModel product, IEnumerable<ReviewViewModel> reviews, CustomerViewModel customer)
        {
            Product = product;
            Reviews = reviews;
            Customer = customer;
        }

        public ProductDetailsViewModel Product { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
