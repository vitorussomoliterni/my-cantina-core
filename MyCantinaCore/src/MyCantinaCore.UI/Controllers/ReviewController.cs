using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCantinaCore.DataAccess.Models;
using MyCantinaCore.Services;
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
        [HttpGet("Consumers/{id}/Reviews")]
        public async Task<IActionResult> GetReview(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                var result = await _reviewService.GetAllReviewsByConsumerId(id.Value).ToListAsync();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
