using Microsoft.EntityFrameworkCore;
using MyCantinaCore.Commands.ConsumerBottle;
using MyCantinaCore.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCantinaCore.Services
{
    public class ConsumerBottleApplicationService
    {
        private readonly MyCantinaCoreDbContext _context;

        public ConsumerBottleApplicationService(MyCantinaCoreDbContext context)
        {
            _context = context;
        }

        public async Task<ConsumerBottle> AddConsumerBottle(ConsumerBottleCommand command)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Id == command.ConsumerId);
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == command.BottleId);

            if (consumer == null || bottle == null)
                throw new InvalidOperationException($"No consumer bottle found for consumer id {command.ConsumerId} and bottle id {command.BottleId}");

            var consumerBottle = new ConsumerBottle()
            {
                ConsumerId = command.ConsumerId,
                BottleId = command.BottleId,
                DateAcquired = command.DateAcquired,
                DateOpened = command.DateOpened,
                Qty = command.Qty,
                Owned = command.Owned,
                PricePaid = command.PricePaid
            };

            await _context.ConsumerBottles.AddAsync(consumerBottle);
            await _context.SaveChangesAsync();

            return consumerBottle;
        }

        public async Task<ConsumerBottle> UpdateConsumerBottle(ConsumerBottleCommand command)
        {
            var consumer = await _context.Consumers.FirstOrDefaultAsync(c => c.Id == command.ConsumerId);
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == command.BottleId);

            if (consumer == null || bottle == null)
                throw new InvalidOperationException($"No consumer bottle found for consumer id {command.ConsumerId} and bottle id {command.BottleId}");

            var consumerBottle = _context.ConsumerBottles.FirstOrDefault(cb => cb.BottleId == command.BottleId && cb.ConsumerId == command.ConsumerId);

            if (consumerBottle == null)
                throw new InvalidOperationException("No consumer bottle found");

            consumerBottle.DateAcquired = command.DateAcquired;
            consumerBottle.DateOpened = command.DateOpened;
            consumerBottle.Qty = command.Qty;
            consumerBottle.Owned = command.Owned;
            consumerBottle.PricePaid = command.PricePaid;

            _context.ConsumerBottles.Update(consumerBottle);
            await _context.SaveChangesAsync();

            return consumerBottle;
        }

        public async Task DeleteConsumerBottle(int consumerId, int bottleId)
        {
            var consumerBottle = _context.ConsumerBottles.FirstOrDefault(cb => cb.BottleId == bottleId && cb.ConsumerId == consumerId);

            if (consumerBottle == null)
                throw new InvalidOperationException("No consumer bottle found");

            _context.ConsumerBottles.Remove(consumerBottle);
            await _context.SaveChangesAsync();
        }

        public IQueryable<ConsumerBottle> GetAllConsumerBottlesByConsumerId(int consumerId)
        {
            var consumerBottles = _context.ConsumerBottles.Where(cb => cb.ConsumerId == consumerId);

            return consumerBottles;
        }

        public ConsumerBottle GetConsumerBottle(int bottleId, int consumerId)
        {
            var consumerBottle = _context.ConsumerBottles.FirstOrDefault(cb => cb.BottleId == bottleId && cb.ConsumerId == consumerId);

            if (consumerBottle == null)
                throw new InvalidOperationException("No consumer bottle found");

            return consumerBottle;
        }
    }
}
