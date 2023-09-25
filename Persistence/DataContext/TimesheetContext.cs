using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataContext
{
    public class TimesheetContext:DbContext 
    {
        public TimesheetContext(DbContextOptions<TimesheetContext> options):base(options) {}
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Client> Clients { get; set; }

    }
}
