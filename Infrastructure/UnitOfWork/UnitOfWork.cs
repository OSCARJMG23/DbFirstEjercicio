using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FormulaRaceContext _context;

        private IDriverRepository _drivers;
        private ITeamRepository _teams;

        public UnitOfWork(FormulaRaceContext context)
        {
            _context = context;
        }
        
        public IDriverRepository Drivers
        {
            get{
                if(_drivers == null)
                {
                    _drivers = new DriverRepository(_context);
                }
                return _drivers;
            }
        }
        public ITeamRepository Teams
        {
            get{
                if(_teams == null)
                {
                    _teams = new TeamRepository(_context);
                }
                return _teams;
            }
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}