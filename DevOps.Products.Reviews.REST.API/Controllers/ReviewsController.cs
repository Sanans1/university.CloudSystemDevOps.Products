using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevOps.Products.Common.Repository;
using DevOps.Products.Reviews.DAL;
using DevOps.Products.Reviews.REST.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevOps.Products.Reviews.REST.API.Controllers
{
    [Route("api/review-management/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IGenericRepository<Review, ReviewDTO> _reviewRepository;

        public ReviewsController(IGenericRepository<Review, ReviewDTO> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews(int? productID)
        {
            return Ok(await _reviewRepository.Get(review => !productID.HasValue || review.ProductID == productID));
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(int id)
        {
            ReviewDTO review = await _reviewRepository.GetByID(id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDTO review)
        {
            if (id != review.ID)
            {
                return BadRequest();
            }

            try
            {
                await _reviewRepository.Update(id, review);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _reviewRepository.EntityExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Reviews
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReview(ReviewDTO review)
        {
            await _reviewRepository.Create(review);

            return CreatedAtAction("GetReview", new { id = review.ID }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (await _reviewRepository.EntityExists(id))
            {
                return NotFound();
            }

            await _reviewRepository.Delete(id);

            return NoContent();
        }
    }
}
