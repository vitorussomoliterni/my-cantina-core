using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCantinaCore.Commands.Review;
using MyCantinaCore.DataAccess.Models;
using MyCantinaCore.Services;
using MyCantinaCore.UI.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCantinaCore.UI.Controllers
{
    [Route("api")]
    public class ReviewController : Controller
    {
        private readonly ReviewApplicationService _reviewService;

        public ReviewController(ReviewApplicationService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api / Consumers / Id / Reviews
        [HttpGet("Consumers/{ConsumerId}/Reviews")]
        public async Task<IActionResult> GetReviewsByConsumerId(int? consumerId)
        {
            if (consumerId == null)
                return BadRequest();

            try
            {
                var reviews = _reviewService.GetAllReviewsByConsumerId(consumerId.Value);

                var result = await reviews.Select(r => new ReviewDetailsViewModel()
                {
                    Id = r.Id,
                    Body = r.Body,
                    Rating = r.Rating,
                    BottleName = r.Bottle.Name,
                    Producer = r.Bottle.Producer,
                    BottleYear = r.Bottle.Year,
                    WineType = r.Bottle.WineType,
                    ConsumerFullName = $"{r.Consumer.FirstName} {r.Consumer.LastName}",
                    ConsumerId = r.ConsumerId,
                    BottleId = r.BottleId,
                    DatePosted = r.DatePosted,
                    DateModified = r.DateModified
                }).ToListAsync();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api / Bottles / BottleId / Reviews
        [HttpGet("Bottles/{BottleId}/Reviews")]
        public async Task<IActionResult> GetReviewsByBottleId(int? bottleId)
        {
            if (bottleId == null)
                return BadRequest();

            try
            {
                var reviews = _reviewService.GetAllReviewsByBottleId(bottleId.Value);

                var result = await reviews.Select(r => new ReviewDetailsViewModel()
                {
                    Id = r.Id,
                    Body = r.Body,
                    Rating = r.Rating,
                    BottleName = r.Bottle.Name,
                    Producer = r.Bottle.Producer,
                    BottleYear = r.Bottle.Year,
                    WineType = r.Bottle.WineType,
                    ConsumerFullName = $"{r.Consumer.FirstName} {r.Consumer.LastName}",
                    ConsumerId = r.ConsumerId,
                    BottleId = r.BottleId,
                    DatePosted = r.DatePosted,
                    DateModified = r.DateModified
                }).FirstOrDefaultAsync();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api / Reviews / Id
        [HttpGet("Reviews/{ReviewId}")]
        public async Task<IActionResult> GetReviewByReviewId(int? reviewId)
        {
            if (reviewId == null)
                return BadRequest();

            try
            {
                var reviews = _reviewService.GetReview(reviewId.Value);

                var result = await reviews.Select(r => new ReviewDetailsViewModel()
                {
                    Id = r.Id,
                    Body = r.Body,
                    Rating = r.Rating,
                    BottleName = r.Bottle.Name,
                    Producer = r.Bottle.Producer,
                    BottleYear = r.Bottle.Year,
                    WineType = r.Bottle.WineType,
                    ConsumerFullName = $"{r.Consumer.FirstName} {r.Consumer.LastName}",
                    ConsumerId = r.ConsumerId,
                    BottleId = r.BottleId,
                    DatePosted = r.DatePosted,
                    DateModified = r.DateModified
                }).FirstOrDefaultAsync();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api / Consumers / {ConsumerId} / Bottle / {BottleId} / Reviews
        [HttpPost("Consumers/{ConsumerId}/Bottles/{BottleId}/Reviews")]
        public async Task<IActionResult> CreateReview(int? consumerId, int? bottleId, [FromBody] ReviewCreateViewModel model)
        {
            if (consumerId == null || bottleId == null || model == null)
                return BadRequest();

            var command = new ReviewCommand()
            {
                BottleId = bottleId.Value,
                ConsumerId = consumerId.Value,
                Body = model.Body,
                Rating = model.Rating
            };

            try
            {
                var result = await _reviewService.AddReview(command);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api / Reviews / Id
        [HttpPut("Reviews/{Id}")]
        public async Task<IActionResult> UpdateReview(int? id, [FromBody] ReviewCreateViewModel model)
        {
            if (id == null)
                return BadRequest();

            var command = new ReviewCommand()
            {
                Id = id.Value,
                Body = model.Body,
                Rating = model.Rating
            };

            try
            {
                var result = await _reviewService.UpdateReview(command);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api / Reviews / Id
        [HttpDelete("Reviews/{Id}")]
        public async Task<IActionResult> DeleteReview(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                await _reviewService.DeleteReview(id.Value);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
