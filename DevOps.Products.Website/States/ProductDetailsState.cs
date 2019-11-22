using System.Collections.Generic;
using DevOps.Products.Website.Models.ViewModels;
using DevOps.Products.Website.Models.ViewModels.ProductDetails;

namespace DevOps.Products.Website.States
{
    public class ProductDetailsState
    {
        public ProductDetailsState(ProductViewModel product, ICollection<ReviewViewModel> reviews, CustomerViewModel customer)
        {
            Product = product;
            Reviews = reviews;
            Customer = customer;

            ReviewForm = new ReviewViewModel
            {
                ProductID = product.ID,
                Customer = customer
            };
        }

        public ProductViewModel Product { get; set; }
        public ICollection<ReviewViewModel> Reviews { get; set; }
        public CustomerViewModel Customer { get; set; }
        public ReviewViewModel ReviewForm { get; set; }
    }
}
