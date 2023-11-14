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
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly FormulaRaceContext _context;

        public TeamRepository(FormulaRaceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Team> GetTeamAsync(string teamName)
        {
            return await _context.Teams
                    .FirstOrDefaultAsync(e=> e.Name.ToLower() == teamName.ToLower());
                    
        }
    }
}