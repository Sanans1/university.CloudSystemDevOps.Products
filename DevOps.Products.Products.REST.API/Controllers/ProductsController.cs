using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevOps.Products.Common.Repository;
using DevOps.Products.Products.DAL;
using DevOps.Products.Products.REST.API.Models;
using DevOps.Products.Products.REST.API.Services.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Products.Products.REST.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly BrandRepository _brandRepository;
        private readonly PriceHistoryRepository _priceHistoryRepository;

        public ProductsController(IMapper mapper, ProductRepository productRepository,
            CategoryRepository categoryRepository, BrandRepository brandRepository,
            PriceHistoryRepository priceHistoryRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _priceHistoryRepository = priceHistoryRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string searchString, int? categoryID, int? brandID)
        {
            return Ok(await _productRepository.Get(filter: product => (string.IsNullOrWhiteSpace(searchString) || 
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
                ProductDTO oldProduct = await _productRepository.GetByID(id);

                if (product.Price != oldProduct.Price)
                {
                    await _priceHistoryRepository.Create(_mapper.Map(oldProduct, new PriceHistoryDTO()));
                }

                product = await CreateAndSetCategory(product);
                product = await CreateAndSetBrand(product);

                await _productRepository.Update(id, product);
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
            product = await CreateAndSetCategory(product);
            product = await CreateAndSetBrand(product);

            ProductDTO productCreated = await _productRepository.Create(product);

            return CreatedAtAction("GetProduct", new { id = productCreated.ID }, productCreated);
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

        private async Task<ProductDTO> CreateAndSetCategory(ProductDTO product)
        {
            IEnumerable<CategoryDTO> categories = await _categoryRepository.Get(category => category.Name == product.CategoryName);

            if (categories.Any())
            {
                product.CategoryID = categories.First().ID;
            }
            else
            {
                CategoryDTO categoryDTO = await _categoryRepository.Create(_mapper.Map(product, new CategoryDTO()));
                product.CategoryID = categoryDTO.ID;
            }

            return product;
        }

        private async Task<ProductDTO> CreateAndSetBrand(ProductDTO product)
        {
            IEnumerable<BrandDTO> brands = await _brandRepository.Get(brand => brand.Name == product.BrandName);

            if (brands.Any())
            {
                product.BrandID = brands.First().ID;
            }
            else
            {
                BrandDTO brandDTO = await _brandRepository.Create(_mapper.Map(product, new BrandDTO()));
                product.BrandID = brandDTO.ID;
            }

            return product;
        }
    }
}
