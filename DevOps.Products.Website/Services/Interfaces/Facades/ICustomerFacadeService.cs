using System.Threading.Tasks;
using DevOps.Products.DTOs;

namespace DevOps.Products.Website.Services.Interfaces.Facades
{
    public interface ICustomerFacadeService
    {
        Task<CustomerDTO> GetCurrentCustomer();
        Task<CustomerDTO> GetCustomer(int id);
    }
}