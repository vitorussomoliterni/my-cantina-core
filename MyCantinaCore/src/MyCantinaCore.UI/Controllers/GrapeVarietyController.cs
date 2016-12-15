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
    [Route("api")]
    public class GrapeVarietyController : Controller
    {
        private readonly GrapeVarietyApplicationService _grapeVarietyService;

        public GrapeVarietyController(GrapeVarietyApplicationService grapeVarietyService)
        {
            _grapeVarietyService = grapeVarietyService;
        }

        // GET: api / GrapeVariety / id
        [HttpGet("[controller]/{id}")]
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

        // GET: api / GrapeVarieties
        [HttpGet("GrapeVarieties")]
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

        // DELETE: api / GrapeVariety / Delete / id
        [HttpDelete("[controller]/Delete/{id}")]
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

        // POST: api / GrapeVariety / Add
        [HttpPost("[controller]/Add")]
        public async Task<IActionResult> Create([FromBody] GrapeVarietyViewModel model)
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

        // PUT: api / GrapeVariety / Edit / id
        [HttpPut("[controller]/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id, [FromBody] GrapeVarietyViewModel model)
        {
            if (id == null || model == null)
                return BadRequest();

            try
            {
                var grapeVariety = await _grapeVarietyService.UpdateGrapeVariety(id.Value, model.Name, model.Colour);
                return new ObjectResult(grapeVariety);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
