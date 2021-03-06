﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCantinaCore.Commands.ConsumerBottle;
using MyCantinaCore.Services;
using MyCantinaCore.UI.ViewModels.ConsumerBottle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCantinaCore.UI.Controllers
{
    [Route("api/ConsumerBottles")]
    public class ConsumerBottleController : Controller
    {
        private readonly ConsumerBottleApplicationService _consumerBottleService;

        public ConsumerBottleController(ConsumerBottleApplicationService consumerBottleService)
        {
            _consumerBottleService = consumerBottleService;
        }

        // GET: api / ConsumerBottles / ConsumerId
        [HttpGet("{consumerId}")]
        public async Task<IActionResult> Get(int? consumerId)
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
                return BadRequest(ex);
            }
        }

        // GET: api / ConsumerBottles / ConsumerId / BottleId
        [HttpGet("{ConsumerId}/{BottleId}")]
        public async Task<IActionResult> Get(int? consumerId, int? bottleId)
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
                    PricePaid = cb.PricePaid,
                    DateAcquired = cb.DateAcquired,
                    DateOpened = cb.DateOpened
                }).FirstOrDefaultAsync();

                var grapeVarieties = await consumerBottle.Select(cb => cb.Bottle.BottleGrapeVarieties).FirstOrDefaultAsync();

                result.GrapeVarieties.AddRange(grapeVarieties.Select(gv => gv.GrapeVarietyName));

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api / ConsumerBottles / ConsumerId
        [HttpPost("{ConsumerId}")]
        public async Task<IActionResult> Post(int? consumerId, [FromBody] ConsumerCreateViewModel model)
        {
            if (consumerId == null || model == null)
                return BadRequest();

            var command = new ConsumerBottleCommand()
            {
                ConsumerId = consumerId.Value,
                BottleId = model.BottleId,
                Owned = model.Owned,
                DateAcquired = model.DateAcquired,
                DateOpened = model.DateOpened,
                PricePaid = model.PricePaid,
                Qty = model.Qty
            };

            try
            {
                var result = await _consumerBottleService.AddConsumerBottle(command);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api / ConsumerBottles / ConsumerId / BottleId
        [HttpPut("{ConsumerId}/{BottleId}")]
        public async Task<IActionResult> Put(int? consumerId, int? bottleId, [FromBody] ConsumerCreateViewModel model)
        {
            if (consumerId == null || bottleId == null || model == null)
                return BadRequest();

            var command = new ConsumerBottleCommand()
            {
                BottleId = bottleId.Value,
                ConsumerId = consumerId.Value,
                DateAcquired = model.DateAcquired,
                DateOpened = model.DateOpened,
                Owned = model.Owned,
                PricePaid = model.PricePaid,
                Qty = model.Qty
            };

            try
            {
                var result = await _consumerBottleService.UpdateConsumerBottle(command);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api / ConsumerBottles / ConsumerId / BottleId
        [HttpDelete("{ConsumerId}/{BottleId}")]
        public async Task<IActionResult> Delete(int? consumerId, int? bottleId)
        {
            if (consumerId == null || bottleId == null)
                return BadRequest();

            try
            {
                await _consumerBottleService.DeleteConsumerBottle(consumerId.Value, bottleId.Value);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
