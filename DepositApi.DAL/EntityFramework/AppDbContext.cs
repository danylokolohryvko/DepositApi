using DepositApi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DepositApi.DAL.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public DbSet<DepositModel> deposits;

        public DbSet<DepositCalculationModel> depositCalculations;

        public AppDbContext(DbContextOptions options) : base(options) 
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DepositModel>()
                .ToTable("Deposits");
            modelBuilder.Entity<DepositCalculationModel>()
                .ToTable("DepositCalcs");
        }
    }
}
