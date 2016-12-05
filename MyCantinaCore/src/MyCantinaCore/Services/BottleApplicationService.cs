using System;
using System.Collections.Generic;
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

            var grapeVarieties = new List<GrapeVariety>();

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
    }
}
