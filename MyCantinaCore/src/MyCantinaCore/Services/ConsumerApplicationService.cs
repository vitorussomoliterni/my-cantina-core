using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCantinaCore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using MyCantinaCore.Commands.Consumer;

namespace MyCantinaCore.Services
{
    public class ConsumerApplicationService
    {
        private readonly MyCantinaCoreDbContext _context;

        public ConsumerApplicationService(MyCantinaCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Consumer> AddConsumer(AddCounsumerCommand command)
        {
            var consumer = new Consumer()
            {
                FirstName = command.FristName,
                LastName = command.LastName,
                DateOfBirth = command.DateOfBirth,
                Email = command.Email
            };

            await _context.Consumers.AddAsync(consumer);
            await _context.SaveChangesAsync();

            return consumer;
        }

        public async Task<Consumer> UpdateConsumer(UpdateConsumerCommand command)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Id == command.Id);

            if (consumer == null)
                throw new InvalidOperationException($"No consumer found with id {command.Id}");

            consumer.FirstName = command.FristName;
            consumer.LastName = command.LastName;
            consumer.DateOfBirth = command.DateOfBirth;
            consumer.Email = command.Email;

            await _context.SaveChangesAsync();

            return consumer;
        }

        public async Task RemoveConsumer(int id)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Id == id);

            if (consumer == null)
                throw new InvalidOperationException($"No consumer found with id {id}");

            _context.Consumers.Remove(consumer);

            await _context.SaveChangesAsync();
        }

        public IQueryable<Consumer> GetAllConsumers()
        {
            var consumers = _context.Consumers;

            return consumers;
        }

        public async Task<Consumer> GetConsumer(int id)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Id == id);

            if (consumer == null)
                throw new InvalidOperationException($"No consumer found with id {id}");

            return consumer;
        }
    }
}
