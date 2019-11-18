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
    public class BrandsController : ControllerBase
    {
        private readonly IGenericRepository<Brand, BrandDTO> _brandRepository;

        public BrandsController(IGenericRepository<Brand, BrandDTO> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            return Ok(await _brandRepository.Get());
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDTO>> GetBrand(int id)
        {
            BrandDTO brand = await _brandRepository.GetByID(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, BrandDTO brand)
        {
            if (id != brand.ID)
            {
                return BadRequest();
            }

            try
            {
                await _brandRepository.Update(id, brand);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _brandRepository.EntityExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Brands
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BrandDTO>> PostBrand(BrandDTO brand)
        {
            await _brandRepository.Create(brand);

            return CreatedAtAction("GetBrand", new { id = brand.ID }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (await _brandRepository.EntityExists(id))
            {
                return NotFound();
            }

            await _brandRepository.Delete(id);

            return NoContent();
        }
    }
}
