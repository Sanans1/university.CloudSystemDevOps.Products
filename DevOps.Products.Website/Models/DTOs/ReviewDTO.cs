namespace DevOps.Products.Website.Models.DTOs
{
    public class ReviewDTO
    {
        public int ID { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }

        public int ProductID { get; set; }
        public string CustomerUsername { get; set; }
    }
}
