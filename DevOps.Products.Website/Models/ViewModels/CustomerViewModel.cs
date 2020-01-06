using System.ComponentModel.DataAnnotations;

namespace DevOps.Products.Website.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string DeliveryAddress { get; set; }
        public string TelephoneNumber { get; set; }
        public bool CanPurchase { get; set; }
    }
}
