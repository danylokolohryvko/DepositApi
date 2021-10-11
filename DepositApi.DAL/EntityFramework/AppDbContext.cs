﻿using DepositApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DepositApi.DAL.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public DbSet<Deposit> deposits;

        public DbSet<DepositCalculation> depositCalculations;

        public AppDbContext(DbContextOptions options) : base(options) 
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Deposit>()
                .ToTable("Deposits");
            modelBuilder.Entity<DepositCalculation>()
                .ToTable("DepositCalcs");
        }
    }
}
