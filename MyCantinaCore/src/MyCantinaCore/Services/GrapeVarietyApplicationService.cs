using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCantinaCore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<GrapeVariety> UpdateGrapeVariety(int id, string name, string colour)
        {
            var grapeVariety = await _context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == id);

            if (grapeVariety == null)
                throw new InvalidOperationException($"No grape variety found for id {id}");

            grapeVariety.Name = name;
            grapeVariety.Colour = colour;

            await _context.SaveChangesAsync();

            return grapeVariety;
        }

        public IQueryable<GrapeVariety> GetAllGrapeVarieties()
        {
            var grapeVarieties = _context.GrapeVarieties;

            return grapeVarieties;
        }

        public async Task<GrapeVariety> GetGrapeVariety(int id)
        {
            var grapeVariety = await _context.GrapeVarieties.FirstOrDefaultAsync(gv => gv.Id == id);

            if (grapeVariety == null)
                throw new InvalidOperationException($"No grape variety found for id {id}");
            
            return grapeVariety;
        }
    }
}
