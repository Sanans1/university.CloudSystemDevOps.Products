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
    public class PriceHistoriesController : ControllerBase
    {
        private readonly PriceHistoryRepository _priceHistoryRepository;

        public PriceHistoriesController(PriceHistoryRepository priceHistoryRepository)
        {
            _priceHistoryRepository = priceHistoryRepository;
        }

        // GET: api/PriceHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceHistoryDTO>>> GetPriceHistories(int? productID)
        {
            return Ok(await _priceHistoryRepository.Get(priceHistory => !productID.HasValue || priceHistory.ProductID == productID));
        }

        // GET: api/PriceHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceHistoryDTO>> GetPriceHistory(int id)
        {
            PriceHistoryDTO priceHistory = await _priceHistoryRepository.GetByID(id);

            if (priceHistory == null)
            {
                return NotFound();
            }

            return Ok(priceHistory);
        }

        // PUT: api/PriceHistories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriceHistory(int id, PriceHistoryDTO priceHistory)
        {
            if (id != priceHistory.ID)
            {
                return BadRequest();
            }

            try
            {
                await _priceHistoryRepository.Update(id, priceHistory);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _priceHistoryRepository.EntityExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/PriceHistories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PriceHistoryDTO>> PostPriceHistory(PriceHistoryDTO priceHistory)
        {
            await _priceHistoryRepository.Create(priceHistory);

            return CreatedAtAction("GetPriceHistory", new { id = priceHistory.ID }, priceHistory);
        }

        // DELETE: api/PriceHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceHistory(int id)
        {
            if (!await _priceHistoryRepository.EntityExists(id))
            {
                return NotFound();
            }

            await _priceHistoryRepository.Delete(id);

            return NoContent();
        }
    }
}
