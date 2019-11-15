using System;

namespace DevOps.Products.PriceHistories.REST.API.Models
{
    public class PriceHistoryDTO
    {
        public int ID { get; set; }
        public decimal Price { get; set; }
        public DateTime DateArchived { get; set; }

        public int ProductID { get; set; }
    }
}
