using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCantinaCore.DataAccess.Models;

namespace MyCantinaCore.Services
{
    public class GrapeVarietyApplicationService
    {
        private readonly MyCantinaCoreDbContext _context;

        public GrapeVarietyApplicationService(MyCantinaCoreDbContext context)
        {
            _context = context;
        }

        public async Task<GrapeVariety> AddGrapeVariety(string name, string colour)
        {
            var grapeVariety = new GrapeVariety()
            {
                Name = name,
                Colour = colour
            };

            await _context.GrapeVarieties.AddAsync(grapeVariety);
            await _context.SaveChangesAsync();

            return grapeVariety;
        }
    }
}
