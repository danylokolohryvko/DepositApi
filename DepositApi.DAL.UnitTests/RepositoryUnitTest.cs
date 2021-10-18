using DepositApi.DAL.Repository;
using DepositApi.DAL.UnitTests.TestModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;
using DepositApi.DAL.UnitTests.TestContext;
using System.Linq;

namespace DepositApi.DAL.UnitTests
{
    [TestFixture]
    public class RepositoryUnitTest
    {
        private DbContextOptions options;

        public RepositoryUnitTest()
        {
            this.options = new DbContextOptionsBuilder<TestDbContext>().UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DepositApiTest;Trusted_Connection=True").Options;
            using (var context = new TestDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Set<TestModel>().AddRange(new TestModel[] {
                    new TestModel { Name = "Item" },
                    new TestModel { Name = "Second" },
                    new TestModel { Name = "Third" },
                    new TestModel { Name = "Fourth" },
                });
                context.SaveChanges();
            }
        }

        [Test]
        public async Task CreateAsync_TestModelObjectPassed_ProperMethodCalled()
        {
            var model = new TestModel { Name = "0"};

            using (var context = new TestDbContext(options))
            {
                var repository = new Repository<TestModel>(context);
                await repository.CreateAsync(model);
            }

            using (var context = new TestDbContext(options))
            {
                var item = context.Set<TestModel>().Single(e => e.Name == "0");
                Assert.AreEqual(item.Id, model.Id);
            }
        }

        [Test]
        public async Task CreateRangeAsync_TestModelObjectPassed_ProperMethodCalled()
        {
            //Arrange
            var models = GetTestModels();

            using (var context = new TestDbContext(options))
            {
                var repository = new Repository<TestModel>(context);
                await repository.CreateRangeAsync(models);
            }

            using (var context = new TestDbContext(options))
            {
                var items = context.Set<TestModel>().Where(e => models.Contains(e)).ToList();

                Assert.AreEqual(items[0].Id, models[0].Id);
                Assert.AreEqual(items[0].Name, models[0].Name);

                Assert.AreEqual(items[1].Id, models[1].Id);
                Assert.AreEqual(items[1].Name, models[1].Name);

                Assert.AreEqual(items[2].Id, models[2].Id);
                Assert.AreEqual(items[2].Name, models[2].Name);
            }
        }

        [Test]
        public async Task FindAsync_TestModelObjectPassed_ProperMethodCalled()
        {
            //Arrange
            TestModel model;

            using (var context = new TestDbContext(options))
            {
                model = context.Set<TestModel>().Single(e => e.Name == "Item");
            }

            using (var context = new TestDbContext(options))
            {
                var repository = new Repository<TestModel>(context);
                var item = await repository.FindAsync(model.Id);
                Assert.AreEqual(item.Name, model.Name);
            }
        }

        [Test]
        public async Task FindRangeAsync_TestModelObjectPassed_ProperMethodCalled()
        {
            var models = new List<TestModel>();

            using (var context = new TestDbContext(options))
            {
                models.Add(context.Set<TestModel>().Single(e => e.Name == "Second"));
                models.Add(context.Set<TestModel>().Single(e => e.Name == "Third"));
            }

            using (var context = new TestDbContext(options))
            {
                var repository = new Repository<TestModel>(context);
                var items = await repository.FindRangeAsync(m => m.Name.Length > 4, 0, 2);

                Assert.AreEqual(items[0].Id, models[0].Id);
                Assert.AreEqual(items[0].Name, models[0].Name);

                Assert.AreEqual(items[1].Id, models[1].Id);
                Assert.AreEqual(items[1].Name, models[1].Name);

                Assert.AreEqual(items.Count, 2);
            }
        }

        [Test]
        public async Task UpdateAsync_TestModelObjectPassed_ProperMethodCalled()
        {
            TestModel model;

            using (var context = new TestDbContext(options))
            {
                model = context.Set<TestModel>().Single(e => e.Name == "Item");
                model.Name = "First";
                var repository = new Repository<TestModel>(context);
                await repository.UpdateAsync(model);
            }

            using (var context = new TestDbContext(options))
            {
                var item = context.Set<TestModel>().Single(e => e.Id == model.Id);
                Assert.AreEqual(item.Name, model.Name);

                item = context.Set<TestModel>().FirstOrDefault(e => e.Name == "Item");
                Assert.IsNull(item);
            }
        }

        [Test]
        public async Task DeleteAsync_TestModelObjectPassed_ProperMethodCalled()
        {
            TestModel model;

            using (var context = new TestDbContext(options))
            {
                model = context.Set<TestModel>().Single(e => e.Name == "Fourth");
                Assert.IsNotNull(model);
                var repository = new Repository<TestModel>(context);
                await repository.DeleteAsync(model.Id);
            }

            using (var context = new TestDbContext(options))
            {
                var item = context.Set<TestModel>().FirstOrDefault(e => e.Id == model.Id);
                Assert.IsNull(item);
            }
        }

        private List<TestModel> GetTestModels()
        {
            return new List<TestModel>
            {
                new TestModel { Name = "1" },
                new TestModel { Name = "2" },
                new TestModel { Name = "3" },
            };
        }
    }
}
