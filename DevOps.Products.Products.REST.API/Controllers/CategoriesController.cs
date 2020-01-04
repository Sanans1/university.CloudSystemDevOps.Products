using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoriesController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            return Ok(await _categoryRepository.Get());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            CategoryDTO category = await _categoryRepository.GetByID(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }

            try
            {
                await _categoryRepository.Update(id, category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _categoryRepository.EntityExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO category)
        {
            await _categoryRepository.Create(category);

            return CreatedAtAction("GetCategory", new { id = category.ID }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!await _categoryRepository.EntityExists(id))
            {
                return NotFound();
            }

            await _categoryRepository.Delete(id);

            return NoContent();
        }
    }
}
