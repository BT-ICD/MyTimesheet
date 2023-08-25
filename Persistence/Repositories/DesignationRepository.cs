using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly TimesheetContext _context;

        public DesignationRepository(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Designation>> GetAllDesignationAsync()
        {
            return await _context.Designations.ToListAsync();
        }

        public async Task<Designation?> GetDesignationById(int designationId)
        {
            return await _context.Designations.Where(x=>x.DesignationId== designationId).FirstOrDefaultAsync();
        }
    }
}
