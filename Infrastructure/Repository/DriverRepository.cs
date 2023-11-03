using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        private readonly FormulaRaceContext _context;

        public DriverRepository(FormulaRaceContext context) : base(context)
        {
            _context = context;
        }
    }
}