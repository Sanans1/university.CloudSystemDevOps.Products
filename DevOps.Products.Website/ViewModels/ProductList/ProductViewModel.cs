namespace DevOps.Products.Website.ViewModels.ProductList
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }

        public CategoryViewModel Category { get; set; }
        public BrandViewModel Brand { get; set; }
    }
}
