using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCantinaCore.Services;
using MyCantinaCore.UI.ViewModels;

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
        public IActionResult GetAll()
        {
            try
            {
                var bottles = _bottleService.GetAllBottles().ToList();
                return new ObjectResult(bottles);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET / Bottles / Id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                var bottle = await _bottleService.GetBottle(id.Value);
                return new ObjectResult(bottle);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT / Bottles / BottleId / GrapeVarieties / GrapeVarietyId
        [HttpPut("{BottleId}/GrapeVarieties/{GrapeVarietyId}")]
        public async Task<IActionResult> AddGrapeVarietyToBottle(int? bottleId, int? grapeVarietyId)
        {
            if (bottleId == null || grapeVarietyId == null)
                return BadRequest();

            try
            {
                var bottle = await _bottleService.AddGrapeVarietyToBottle(bottleId.Value, grapeVarietyId.Value);
                return new ObjectResult(bottle);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
