using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCantinaCore.Services;
using MyCantinaCore.DataAccess.Models;

namespace MyCantinaCore.UI.Controllers
{
    [Route("api/[controller]")]
    public class GrapeVarietyController : Controller
    {
        private readonly GrapeVarietyApplicationService _grapeVarietyService;

        public GrapeVarietyController(GrapeVarietyApplicationService grapeVarietyService)
        {
            _grapeVarietyService = grapeVarietyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                var grapeVariety = await _grapeVarietyService.GetGrapeVariety(id.Value);
                return new ObjectResult(grapeVariety);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
