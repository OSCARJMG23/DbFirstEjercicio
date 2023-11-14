using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        private readonly FormulaRaceContext _context;

        public DriverRepository(FormulaRaceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Driver> GetDriverNameAsync(string drivername)
        {
            return await _context.Drivers
                    .Include(e=>e.Teams)
                    .FirstOrDefaultAsync(e=>e.Name.ToLower()== drivername.ToLower());
        }
    }
}