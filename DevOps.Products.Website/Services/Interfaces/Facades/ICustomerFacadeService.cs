using System.Threading.Tasks;
using DevOps.Products.Website.Models.DTOs;

namespace DevOps.Products.Website.Services.Interfaces.Facades
{
    public interface ICustomerFacadeService
    {
        Task<CustomerDTO> GetCustomer(string customerUsername);
    }
}
