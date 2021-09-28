using DepositApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepositApi.DAL.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public DbSet<Deposit> deposits;

        public DbSet<DepositCalc> depositCalcs;

        public AppDbContext(DbContextOptions options) : base(options) 
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Deposit>()
                .ToTable("Deposits", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<DepositCalc>()
                .ToTable("DepositCalcs", t => t.ExcludeFromMigrations());
        }
    }
}
