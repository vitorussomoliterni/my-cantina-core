using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCantinaCore.Services;
using MyCantinaCore.UI.ViewModels;

namespace MyCantinaCore.UI.Controllers
{
    [Route("api/GrapeVarieties")]
    public class GrapeVarietyController : Controller
    {
        private readonly GrapeVarietyApplicationService _grapeVarietyService;

        public GrapeVarietyController(GrapeVarietyApplicationService grapeVarietyService)
        {
            _grapeVarietyService = grapeVarietyService;
        }

        // GET: api / GrapeVarieties / id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrapeVariety(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                var result = await _grapeVarietyService.GetGrapeVariety(id.Value);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // GET: api / GrapeVarieties
        [HttpGet]
        public IActionResult GetAllGrapeVarieties()
        {
            try
            {
                var result = _grapeVarietyService.GetAllGrapeVarieties().ToList();
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // DELETE: api / GrapeVarieties / id
        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGrapeVariety(int? id)
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
                return NotFound(ex);
            }
        }

        // POST: api / GrapeVarieties
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGrapeVariety([FromBody] GrapeVarietyViewModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var result = await _grapeVarietyService.AddGrapeVariety(model.Name, model.Colour);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api / GrapeVarieties / id
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGrapeVariety(int? id, [FromBody] GrapeVarietyViewModel model)
        {
            if (id == null || model == null)
                return BadRequest();

            try
            {
                var result = await _grapeVarietyService.UpdateGrapeVariety(id.Value, model.Name, model.Colour);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
