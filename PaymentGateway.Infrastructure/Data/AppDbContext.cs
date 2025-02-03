using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Domain.Entities.Transaction> Transactions { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }

        // Optional: Override OnModelCreating to configure the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<EventLog>().HasKey(e => e.Id);

           
        }
    }
}
