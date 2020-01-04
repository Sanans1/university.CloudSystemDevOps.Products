using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;

namespace DevOps.Products.Website.Services.Interfaces.Facades
{
    public interface IOrderFacadeService
    {
        Task<IEnumerable<OrderDTO>> GetOrders(string customerUsername, int productID);
    }
}
