namespace DevOps.Products.DTOs
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public CategoryDTO Category { get; set; }
        public BrandDTO Brand { get; set; }
    }
}
