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

            if (command.GrapeVarietyIds != null && command.GrapeVarietyIds.Count > 0)
            {
                var bottleGrapeVarieties = await _context.GrapeVarieties.Where(gv => command.GrapeVarietyIds.Contains(gv.Id)) // Creates new bottle grape variety objects
                .Select(gv => new BottleGrapeVariety
                {
                    Bottle = bottle,
                    GrapeVariety = gv,
                    GrapeVarietyName = gv.Name,
                    GrapeVarietyColour = gv.Colour
                }).ToListAsync();

                bottle.BottleGrapeVarieties.AddRange(bottleGrapeVarieties);
                await _context.AddRangeAsync(bottleGrapeVarieties);
            }

            await _context.Bottles.AddAsync(bottle);
            await _context.SaveChangesAsync();

            return bottle;
        }

        public async Task<Bottle> UpdateBottle(UpdateBottleCommand command)
        {
            var bottle = await _context.Bottles.Include(b => b.BottleGrapeVarieties).FirstOrDefaultAsync(b => b.Id == command.Id);

            if (bottle == null)
                throw new InvalidOperationException($"No bottle found with id {command.Id}");

            bottle.Name = command.Name;
            bottle.Year = command.Year;
            bottle.Producer = command.Producer;
            bottle.Description = command.Description;
            bottle.WineType = command.WineType;
            bottle.Region = command.Region;
            bottle.Country = command.Country;

            var existingGrapeVarieties = bottle.BottleGrapeVarieties.Select(bgv => bgv.GrapeVarietyId);
            var grapeVarietiesToAdd = command.GrapeVarietyIds.Except(existingGrapeVarieties);
            var grapeVarietiesToRemove = existingGrapeVarieties.Except(command.GrapeVarietyIds);

            for(var i = 0; i < existingGrapeVarieties.Count(); i++)
            {
                if (grapeVarietiesToRemove.Contains(bottle.BottleGrapeVarieties[i].GrapeVarietyId))
                {
                    var bgv = bottle.BottleGrapeVarieties[i];
                    bottle.BottleGrapeVarieties.Remove(bgv);
                    //_context.BottleGrapeVarieties.Remove(bgv);
                }
            }

            foreach (var id in grapeVarietiesToAdd)
            {
                var grapeVariety = await _context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == id);
                var bgv = new BottleGrapeVariety()
                {
                    Bottle = bottle,
                    GrapeVariety = grapeVariety,
                    GrapeVarietyName = grapeVariety.Name,
                    GrapeVarietyColour = grapeVariety.Colour
                };

                bottle.BottleGrapeVarieties.Add(bgv);
                //_context.BottleGrapeVarieties.Add(bgv);
            }

            //var bottleGrapeVarieties = await _context.BottleGrapeVarieties.Where(bgv => bgv.BottleId == bottle.Id).ToListAsync();

            //foreach (var id in command.GrapeVarietyIds)
            //{
            //    var grapeVariety = await _context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == id);
            //    var bottleGrapeVariety = new BottleGrapeVariety()
            //    {
            //        Bottle = bottle,
            //        GrapeVariety = grapeVariety,
            //        GrapeVarietyName = grapeVariety.Name,
            //        GrapeVarietyColour = grapeVariety.Colour
            //    };

            //    bottle.BottleGrapeVarieties.Add(bottleGrapeVariety);
            //    await _context.BottleGrapeVarieties.AddAsync(bottleGrapeVariety);
            //}

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

        public IQueryable<Bottle> GetBottle(int id)
        {
            var bottle = _context.Bottles.Where(b => b.Id == id);

            return bottle;
        }

        public async Task<Bottle> AddGrapeVarietyToBottle(int bottleId, int grapeVarietyId)
        {
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == bottleId);
            var grapeVariety = await _context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == grapeVarietyId);

            if (bottle == null || grapeVariety == null)
                throw new InvalidOperationException($"No bottle found with id {bottleId}");

            var bottleGrapeVariety = new BottleGrapeVariety()
            {
                BottleId = bottleId,
                GrapeVarietyId = grapeVarietyId,
                GrapeVarietyName = grapeVariety.Name,
                GrapeVarietyColour = grapeVariety.Colour
            };

            bottle.BottleGrapeVarieties.Add(bottleGrapeVariety);
            grapeVariety.BottleGrapeVarieties.Add(bottleGrapeVariety);

            await _context.SaveChangesAsync();

            return bottle;
        }

        public async Task<Bottle> RemoveGrapeVarietyFromBottle(int bottleId, int grapeVarietyId)
        {
            var bottle = await _context.Bottles.FirstOrDefaultAsync(b => b.Id == bottleId);
            var grapeVariety = await _context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == grapeVarietyId);
            var bottleGrapeVariety = await _context.BottleGrapeVarieties.FirstOrDefaultAsync(bgv => bgv.BottleId == bottleId && bgv.GrapeVarietyId == grapeVarietyId);

            if (bottle == null || grapeVariety == null || bottleGrapeVariety == null)
                throw new InvalidOperationException($"No bottle found with id {bottleId}");

            bottle.BottleGrapeVarieties.Remove(bottleGrapeVariety);
            grapeVariety.BottleGrapeVarieties.Remove(bottleGrapeVariety);
            _context.BottleGrapeVarieties.Remove(bottleGrapeVariety);

            await _context.SaveChangesAsync();

            return bottle;
        }
    }
}
