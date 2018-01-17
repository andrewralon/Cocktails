using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Take02.Models;

namespace Take02.Import
{
    public interface IUnitImporter
    {
        /// <summary>
        /// An idempotent operation which takes in a set of Unit names
        /// and verifies that they all exist in the database
        /// </summary>
        Task ImportUnits(IEnumerable<string> unitNames);
    }

    public class UnitImporter : IUnitImporter
    {
        private readonly CocktailsContext _context;

        public UnitImporter(CocktailsContext context)
        {
            _context = context;
        }

        public async Task ImportUnits(IEnumerable<string> unitNames)
        {
            var existingUnits = await _context.Unit.ToListAsync();

            var newUnits = unitNames
            .Distinct()
            .Except(existingUnits.Select(a => a.Name))
            .Select(a => new Unit
            {
                Name = a
            });

            await _context.Unit.AddRangeAsync(newUnits);
            await _context.SaveChangesAsync();
        }
    }
}