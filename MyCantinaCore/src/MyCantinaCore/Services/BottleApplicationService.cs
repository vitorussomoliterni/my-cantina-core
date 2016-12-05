using System;
using System.Linq;
using System.Threading.Tasks;
using MyCantinaCore.DataAccess.Models;
using MyCantinaCore.Commands.Bottle;
using Microsoft.EntityFrameworkCore;

namespace MyCantinaCore.Services
{
    public class BottleApplicationService
    {
        private readonly MyCantinaCoreDbContext _context;

        public BottleApplicationService(MyCantinaCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Bottle> AddBottle(AddBottleCommand command)
        {
            var bottle = new Bottle()
            {
                Name = command.Name,
                Year = command.Year,
                Producer = command.Producer,
                Description = command.Description,
                WineType = command.WineType,
                Region = command.Region,
                Country = command.Country,
                AverageRating = 0
            };
            
            foreach (var id in command.GrapeVarietyIds)
            {
                var grapeVariety = await _context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == id);
                var bottleGrapeVariety = new BottleGrapeVariety()
                {
                    Bottle = bottle,
                    GrapeVariety = grapeVariety
                };

                bottle.BottleGrapeVarieties.Add(bottleGrapeVariety);
            }

            await _context.Bottles.AddAsync(bottle);
            await _context.SaveChangesAsync();

            return bottle;
        }

        public async Task<Bottle> UpdateBottle(UpdateBottleCommand command)
        {
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == command.Id);

            if (bottle == null)
                throw new InvalidOperationException($"No bottle found with id {command.Id}");

            bottle.Name = command.Name;
            bottle.Year = command.Year;
            bottle.Producer = command.Producer;
            bottle.Description = command.Description;
            bottle.WineType = command.WineType;
            bottle.Region = command.Region;
            bottle.Country = command.Country;

            bottle.BottleGrapeVarieties.RemoveAll(b => true);

            foreach (var id in command.GrapeVarietyIds)
            {
                var grapeVariety = await _context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == id);
                var bottleGrapeVariety = new BottleGrapeVariety()
                {
                    Bottle = bottle,
                    GrapeVariety = grapeVariety
                };

                bottle.BottleGrapeVarieties.Add(bottleGrapeVariety);
            }

            _context.Bottles.Update(bottle);

            await _context.SaveChangesAsync();

            return bottle;
        }

        public async Task DeleteBottle(int id)
        {
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == id);

            if (bottle == null)
                throw new InvalidOperationException($"No bottle found with id {id}");

            _context.Bottles.Remove(bottle);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Bottle> GetAllBottles()
        {
            var bottles = _context.Bottles;

            return bottles;
        }

        public async Task<Bottle> GetBottle(int id)
        {
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == id);

            return bottle;
        }
    }
}
