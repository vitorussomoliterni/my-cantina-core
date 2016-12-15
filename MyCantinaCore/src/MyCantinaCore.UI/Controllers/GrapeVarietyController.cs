using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCantinaCore.Services;
using MyCantinaCore.DataAccess.Models;
using MyCantinaCore.UI.ViewModels;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var grapeVarieties = _grapeVarietyService.GetAllGrapeVarieties().ToList();
                return new ObjectResult(grapeVarieties);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                await _grapeVarietyService.DeleteGrapeVariety(id.Value);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GrapeVarietyCreateViewModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var grapeVariety = await _grapeVarietyService.AddGrapeVariety(model.Name, model.Colour);
                return new ObjectResult(grapeVariety);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
