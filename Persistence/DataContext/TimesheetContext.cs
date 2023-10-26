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
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }

        public DbSet<TeamMember> TeamMember { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Define one-to-many relationships here
        //    modelBuilder.Entity<Client>()
        //        .HasMany(c => c.Contacts)
        //        .WithOne(cc => cc.Client)
        //        .HasForeignKey(cc => cc.ClientId);

        //    modelBuilder.Entity<Designation>()
        //        .HasMany(d => d.Contacts)
        //        .WithOne(cc => cc.Designation)
        //        .HasForeignKey(cc => cc.DesignationId);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientContact>().HasKey(cc => cc.ContactId); // Define ContactId as the primary key
            modelBuilder.Entity<TeamMember>().HasKey(cc => cc.TeamMemberId);
            // Define other configurations, such as relationships, if needed

            base.OnModelCreating(modelBuilder);
        }

    }
}
