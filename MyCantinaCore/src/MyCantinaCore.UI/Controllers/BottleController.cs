using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCantinaCore.Services;
using MyCantinaCore.UI.ViewModels.Bottle;
using Microsoft.EntityFrameworkCore;

namespace MyCantinaCore.UI.Controllers
{
    [Route("api/bottles")]
    public class BottleController : Controller
    {
        private readonly BottleApplicationService _bottleService;

        public BottleController(BottleApplicationService bottleService)
        {
            _bottleService = bottleService;
        }

        // GET / Bottles
        [HttpGet]
        public async Task<IActionResult> GetAllBottles()
        {
            try
            {
                var bottles = _bottleService.GetAllBottles();

                var result = await bottles.Select(b => new BottleDetailsViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    AverageRating = b.AverageRating,
                    Country = b.Country,
                    Producer = b.Producer,
                    Region = b.Region,
                    WineType = b.WineType,
                    Year = b.Year
                }).ToListAsync();

                foreach (var r in result)
                {
                    var bottleGrapeVarieties = await _bottleService.GetBottle(r.Id)
                        .Select(b => b.BottleGrapeVarieties)
                        .FirstOrDefaultAsync();

                    foreach (var bgv in bottleGrapeVarieties)
                    {
                        r.GrapeVarieties.Add(bgv.GrapeVarietyName);
                    }
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET / Bottles / Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBottle(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                var bottle = _bottleService.GetBottle(id.Value);

                var result = await bottle.Select(b => new BottleDetailsViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    AverageRating = b.AverageRating,
                    Country = b.Country,
                    Producer = b.Producer,
                    Region = b.Region,
                    WineType = b.WineType,
                    Year = b.Year
                })
                .FirstOrDefaultAsync();

                var bottleGrapeVarieties = await bottle.Select(b => b.BottleGrapeVarieties).FirstOrDefaultAsync();

                foreach (var bgv in bottleGrapeVarieties)
                {
                    result.GrapeVarieties.Add(bgv.GrapeVarietyName);
                }

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT / Bottles / BottleId / GrapeVarieties / GrapeVarietyId
        [HttpPut("{BottleId}/GrapeVarieties/{GrapeVarietyId}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGrapeVarietyToBottle(int? bottleId, int? grapeVarietyId)
        {
            if (bottleId == null || grapeVarietyId == null)
                return BadRequest();

            try
            {
                var result = await _bottleService.AddGrapeVarietyToBottle(bottleId.Value, grapeVarietyId.Value);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE / Bottles / BottleId / GrapeVarieties / GrapeVarietyId
        [HttpDelete("{BottleId}/GrapeVarieties/{GrapeVarietyId}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveGrapeVarietyToBottle(int? bottleId, int? grapeVarietyId)
        {
            if (bottleId == null || grapeVarietyId == null)
                return BadRequest();

            try
            {
                var result = await _bottleService.RemoveGrapeVarietyFromBottle(bottleId.Value, grapeVarietyId.Value);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
