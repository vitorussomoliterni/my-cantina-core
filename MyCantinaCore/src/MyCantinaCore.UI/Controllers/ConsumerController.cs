using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCantinaCore.Commands.Consumer;
using MyCantinaCore.Services;
using MyCantinaCore.UI.ViewModels.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCantinaCore.UI.Controllers
{
    [Route("api/Consumers")]
    public class ConsumerController : Controller
    {
        private readonly ConsumerApplicationService _consumerService;

        public ConsumerController(ConsumerApplicationService consumerService)
        {
            _consumerService = consumerService;
        }

        // GET: api / Consumers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var consumers = _consumerService.GetAllConsumers();

                var result = await consumers.Select(c => new ConsumerIndexViewModel()
                {
                    Id = c.Id,
                    FullName = $"{c.FirstName} {c.LastName}",
                    Email = c.Email
                }).ToListAsync();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api / Consumers / Id
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                var consumer = await _consumerService.GetConsumer(id.Value);

                var result = new ConsumerDetailsViewModel()
                {
                    Id = consumer.Id,
                    FirstName = consumer.FirstName,
                    LastName = consumer.LastName,
                    DateOfBirth = consumer.DateOfBirth,
                    Email = consumer.Email
                };

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api / Consumers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConsumerCreateViewModel model)
        {
            if (model == null)
                return BadRequest();

            var command = new AddConsumerCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = new DateTime(model.DateOfBirth[0], model.DateOfBirth[1], model.DateOfBirth[2]),
                Email = model.Email
            };

            try
            {
                var result = await _consumerService.AddConsumer(command);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api / Consumers / {Id}
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] ConsumerCreateViewModel model)
        {
            if (model == null || id == null)
                return BadRequest();

            var command = new UpdateConsumerCommand()
            {
                Id = id.Value,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = new DateTime(model.DateOfBirth[0], model.DateOfBirth[1], model.DateOfBirth[2]),
                Email = model.Email
            };

            try
            {
                var result = await _consumerService.UpdateConsumer(command);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api / Consumers / Id
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                await _consumerService.DeleteConsumer(id.Value);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
