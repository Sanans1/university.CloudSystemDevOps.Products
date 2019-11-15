using System.Threading.Tasks;
using DevOps.Products.Composer.API.Models;

namespace DevOps.Products.Composer.API.Services.Interfaces.Facades
{
    public interface ICustomerFacadeService
    {
        Task<CustomerDTO> GetCustomer(int id);
    }
}