using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCantinaCore.Services;
using MyCantinaCore.UI.ViewModels.ConsumerBottle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCantinaCore.UI.Controllers
{
    [Route("api/Consumers/{ConsumerId}/Bottles")]
    public class ConsumerBottleController : Controller
    {
        private readonly ConsumerBottleApplicationService _consumerBottleService;

        public ConsumerBottleController(ConsumerBottleApplicationService consumerBottleService)
        {
            _consumerBottleService = consumerBottleService;
        }

        // GET: api / Consumers / ConsumerId / Bottles
        [HttpGet]
        public async Task<IActionResult> GetAllConsumerBottles(int? consumerId)
        {
            if (consumerId == null)
                return BadRequest();

            try
            {
                var consumerBottles = _consumerBottleService.GetAllConsumerBottlesByConsumerId(consumerId.Value);

                var result = await consumerBottles.Select(cb => new ConsumerBottleIndexViewModel()
                {
                    BottleId = cb.BottleId,
                    ConsumerId = consumerId.Value,
                    BottleName = cb.Bottle.Name,
                    Qty = cb.Qty,
                    Owned = cb.Owned
                }).ToListAsync();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        // GET: api / Consumers / ConsumerId / Bottles / BottleId
        [HttpGet("{BottleId}")]
        public async Task<IActionResult> GetConsumerBottle(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
                return BadRequest();

            try
            {
                var consumerBottle = _consumerBottleService.GetConsumerBottle(bottleId.Value, consumerId.Value);

                var result = await consumerBottle.Select(cb => new ConsumerBottleDetailsViewModel()
                {
                    BottleId = cb.BottleId,
                    ConsumerId = consumerId.Value,
                    BottleName = cb.Bottle.Name,
                    Qty = cb.Qty,
                    Owned = cb.Owned,
                    WineType = cb.Bottle.WineType,
                    Producer = cb.Bottle.Producer,
                    Region = cb.Bottle.Region,
                    Country = cb.Bottle.Country,
                    //PricePaid = cb.PricePaid.Value, // TODO: Fix this conversion, or get rid of it altogether
                    DateAcquired = cb.DateAcquired,
                    DateOpened = cb.DateOpened
                }).FirstOrDefaultAsync();

                var grapeVarieties = await consumerBottle.Select(cb => cb.Bottle.BottleGrapeVarieties).FirstOrDefaultAsync();

                result.GrapeVarieties.AddRange(grapeVarieties.Select(gv => gv.GrapeVarietyName));

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
