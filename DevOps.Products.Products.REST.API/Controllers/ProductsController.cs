using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private const string MANDATORY_INCLUDE_PROPERTIES = "Category,Brand";
        private const string PRICE_HISTORIES_INCLUDE_PROPERTIES = ",PriceHistories";

        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product, ProductDTO> _productRepository;
        private readonly IGenericRepository<Category, CategoryDTO> _categoryRepository;
        private readonly IGenericRepository<Brand, BrandDTO> _brandRepository;
        private readonly IGenericRepository<PriceHistory, PriceHistoryDTO> _priceHistoryRepository;

        public ProductsController(IMapper mapper, IGenericRepository<Product, ProductDTO> productRepository, 
                                  IGenericRepository<Category, CategoryDTO> categoryRepository, IGenericRepository<Brand, BrandDTO> brandRepository, 
                                  IGenericRepository<PriceHistory, PriceHistoryDTO> priceHistoryRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _priceHistoryRepository = priceHistoryRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string searchString, int? categoryID, int? brandID, bool? includePriceHistories)
        {
            string includeProperties = MANDATORY_INCLUDE_PROPERTIES;

            if (includePriceHistories.HasValue && includePriceHistories.Value)
            {
                includeProperties += PRICE_HISTORIES_INCLUDE_PROPERTIES;
            }

            return Ok(await _productRepository.Get(filter: product => (string.IsNullOrWhiteSpace(searchString) || 
                                                                       product.Name.Contains(searchString) || 
                                                                       product.Description.Contains(searchString)) && 
                                                                      (!categoryID.HasValue || product.CategoryID == categoryID) && 
                                                                      (!brandID.HasValue || product.BrandID == brandID), 
                                                   includeProperties: includeProperties));
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
                ProductDTO oldProduct = await _productRepository.GetByID(id);

                if (product.Price != oldProduct.Price)
                {
                    await _priceHistoryRepository.Create(_mapper.Map(product, new PriceHistoryDTO()));
                }

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

            IEnumerable<CategoryDTO> categories = await _categoryRepository.Get(category => category.Name == product.CategoryName);

            if (!categories.Any()) await _categoryRepository.Create(_mapper.Map(product, new CategoryDTO()));

            IEnumerable<BrandDTO> brands = await _brandRepository.Get(brand => brand.Name == product.BrandName);

            if (!brands.Any()) await _brandRepository.Create(_mapper.Map(product, new BrandDTO()));

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

            IEnumerable<PriceHistoryDTO> priceHistories = await _priceHistoryRepository.Get(priceHistory => priceHistory.ProductID == id);

            foreach (PriceHistoryDTO priceHistoryDTO in priceHistories)
            {
                await _priceHistoryRepository.Delete(priceHistoryDTO.ID);
            }

            return NoContent();
        }
    }
}
