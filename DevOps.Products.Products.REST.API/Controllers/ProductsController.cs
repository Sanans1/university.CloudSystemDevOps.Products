using System.Collections.Generic;
using System.Threading.Tasks;
using DevOps.Products.Common.Repository;
using DevOps.Products.Products.DAL;
using DevOps.Products.Products.REST.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Products.Products.REST.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product, ProductDTO> _productRepository;

        public ProductsController(IGenericRepository<Product, ProductDTO> productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string searchString, int? categoryID, int? brandID)
        {
            return Ok(await _productRepository.Get(product => (string.IsNullOrWhiteSpace(searchString) ||
                                                                     product.Name.Contains(searchString) ||
                                                                     product.Description.Contains(searchString)) &&
                                                                    (!categoryID.HasValue || product.CategoryID == categoryID) &&
                                                                    (!brandID.HasValue || product.BrandID == brandID)));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            ProductDTO product = await _productRepository.GetByID(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            try
            {
                await _productRepository.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _productRepository.EntityExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO product)
        {
            await _productRepository.Create(product);

            return CreatedAtAction("GetProduct", new { id = product.ID }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _productRepository.EntityExists(id))
            {
                return NotFound();
            }

            await _productRepository.Delete(id);

            return NoContent();
        }
    }
}
