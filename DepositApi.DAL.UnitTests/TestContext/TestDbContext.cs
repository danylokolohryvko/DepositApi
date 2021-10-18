using DepositApi.DAL.UnitTests.TestModels;
using Microsoft.EntityFrameworkCore;

namespace DepositApi.DAL.UnitTests.TestContext
{
    public class TestDbContext : DbContext
    {
        public DbSet<TestModel> Models { get; set; }

        public TestDbContext(DbContextOptions options) : base(options) { }
    }
}
