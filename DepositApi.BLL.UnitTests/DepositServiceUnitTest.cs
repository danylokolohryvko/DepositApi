using DepositApi.BLL.Services;
using DepositApi.Core.Enums;
using DepositApi.Core.Intrerfaces;
using DepositApi.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DepositApi.BLL.UnitTests
{
    [TestFixture]
    class DepositServiceUnitTest
    {
        private DepositModel DepositModel
        {
            get
            {
                return new DepositModel { Amount = 1000, Percent = 12, Term = 3, UserId = "1", CalculationType = CalculationType.CompoundInterest};
            }
        }

        private List<DepositModel> DepositModelsList
        {
            get
            {
                return new List<DepositModel>
                {
                    DepositModel,
                    new DepositModel { Amount = 2000, Percent = 4, Term = 4, UserId = "1", CalculationType = CalculationType.SimpleInterest},
                    new DepositModel { Amount = 3000, Percent = 3, Term = 5, UserId = "1", CalculationType = CalculationType.CompoundInterest}
                };
            }
        }

        private List<DepositCalculationModel> DepositCalculationsList
        {
            get
            {
                return new List<DepositCalculationModel>
                {
                    new DepositCalculationModel { Month = 1, PercentAdded = 5, TotalAmount = 1005},
                    new DepositCalculationModel { Month = 2, PercentAdded = 5, TotalAmount = 1010},
                    new DepositCalculationModel { Month = 3, PercentAdded = 5, TotalAmount = 1015}
                };
            }
        }

        private Mock<IRepository<DepositModel>> DepositRepositoryMock
        {
            get
            {
                var mock = new Mock<IRepository<DepositModel>>();
                mock.Setup(r => r.FindAsync(It.IsAny<int>())).ReturnsAsync(this.DepositModel);
                mock.Setup(r => r.FindRangeAsync(
                    It.IsAny<Expression<Func<DepositModel, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(this.DepositModelsList);

                return mock;
            }
        }

        private Mock<IRepository<DepositCalculationModel>> DepositCalculationRepositoryMock
        {
            get
            {
                var mock = new Mock<IRepository<DepositCalculationModel>>();
                mock.Setup(r => r.FindRangeAsync(
                    It.IsAny<Expression<Func<DepositCalculationModel, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(DepositCalculationsList);

                return mock;
            }
        }

        [Test]
        public async Task PercentCalculationAsync_SimpleInterest_ProperMethodCall()
        {
            var depositMock = this.DepositRepositoryMock;
            var depositCalculationMock = this.DepositCalculationRepositoryMock;
            var contextMock = GetContextMock("1");
            var service = new DepositService(depositMock.Object, depositCalculationMock.Object, contextMock.Object);
            var model = this.DepositModel;
            model.CalculationType = CalculationType.SimpleInterest;

            var item = await service.PercentCalculationAsync(model);

            Assert.AreEqual(10, item[0].PercentAdded);
            Assert.AreEqual(1010, item[0].TotalAmount);

            Assert.AreEqual(10, item[1].PercentAdded);
            Assert.AreEqual(1020, item[1].TotalAmount);

            Assert.AreEqual(10, item[2].PercentAdded);
            Assert.AreEqual(1030, item[2].TotalAmount);

            depositMock.Verify(r => r.CreateAsync(It.IsAny<DepositModel>()));
            depositCalculationMock.Verify(r => r.CreateRangeAsync(It.IsAny<List<DepositCalculationModel>>()));
        }

        [Test]
        public async Task PercentCalculationAsync_CompoundInterest_ProperMethodCall()
        {
            var depositMock = this.DepositRepositoryMock;
            var depositCalculationMock = this.DepositCalculationRepositoryMock;
            var contextMock = GetContextMock("1");
            var service = new DepositService(depositMock.Object, depositCalculationMock.Object, contextMock.Object);
            var model = this.DepositModel;

            var item = await service.PercentCalculationAsync(model);

            Assert.AreEqual(10, item[0].PercentAdded);
            Assert.AreEqual(1010, item[0].TotalAmount);

            Assert.AreEqual(10.1, item[1].PercentAdded);
            Assert.AreEqual(1020.1, item[1].TotalAmount);

            Assert.AreEqual(10.2, item[2].PercentAdded);
            Assert.AreEqual(1030.3, item[2].TotalAmount);

            depositMock.Verify(r => r.CreateAsync(It.IsAny<DepositModel>()));
            depositCalculationMock.Verify(r => r.CreateRangeAsync(It.IsAny<List<DepositCalculationModel>>()));
        }

        [Test]
        public async Task GetDepositsAsync_WithUserId_ProperMethodCall()
        {
            var depositMock = this.DepositRepositoryMock;
            var depositCalculationMock = this.DepositCalculationRepositoryMock;
            var contextMock = GetContextMock("1");
            var service = new DepositService(depositMock.Object, depositCalculationMock.Object, contextMock.Object);

            var item = await service.GetDepositsAsync(0, 3);

            Assert.IsNotNull(item);
            depositMock.Verify(r => r.FindRangeAsync(It.IsAny<Expression<Func<DepositModel, bool>>>(), It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public async Task GetDepositsAsync_WithoutUserId_ProperMethodCall()
        {
            var depositMock = this.DepositRepositoryMock;
            var depositCalculationMock = this.DepositCalculationRepositoryMock;
            var service = new DepositService(depositMock.Object, depositCalculationMock.Object, GetContextMock(null).Object);

            var item = await service.GetDepositsAsync(0, 3);

            Assert.IsNull(item);
        }

        [Test]
        public async Task GetDepositCalculationsAsync_WithUserId_ProperMethodCall()
        {
            var depositMock = this.DepositRepositoryMock;
            var depositCalculationMock = this.DepositCalculationRepositoryMock;
            var contextMock = GetContextMock("1");
            var service = new DepositService(depositMock.Object, depositCalculationMock.Object, contextMock.Object);

            var item = await service.GetDepositCalculationsAsync(0);

            Assert.IsNotNull(item);
            depositMock.Verify(r => r.FindAsync(It.IsAny<int>()));
            depositCalculationMock.Verify(r => r.FindRangeAsync(It.IsAny<Expression<Func<DepositCalculationModel, bool>>>(), It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public async Task GetDepositCalculationsAsync_WithoutUserId_ProperMethodCall()
        {
            var depositMock = this.DepositRepositoryMock;
            var depositCalculationMock = this.DepositCalculationRepositoryMock;
            var service = new DepositService(depositMock.Object, depositCalculationMock.Object, GetContextMock(null).Object);

            var item = await service.GetDepositCalculationsAsync(0);

            Assert.IsNull(item);
        }

        [Test]
        public async Task GetDepositCalculationCSVAsync_WithUserId_ProperMethodCall()
        {
            var depositMock = this.DepositRepositoryMock;
            var depositCalculationMock = this.DepositCalculationRepositoryMock;
            var contextMock = GetContextMock("1");
            var service = new DepositService(depositMock.Object, depositCalculationMock.Object, contextMock.Object);

            var item = await service.GetDepositCalculationCSVAsync(0);

            Assert.AreEqual("1,5,1005\n2,5,1010\n3,5,1015\n", item);
            depositMock.Verify(r => r.FindAsync(It.IsAny<int>()));
            depositCalculationMock.Verify(r => r.FindRangeAsync(It.IsAny<Expression<Func<DepositCalculationModel, bool>>>(), It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public async Task GetDepositCalculationCSVAsync_WithoutUserId_ProperMethodCall()
        {
            var depositMock = this.DepositRepositoryMock;
            var depositCalculationMock = this.DepositCalculationRepositoryMock;
            var service = new DepositService(depositMock.Object, depositCalculationMock.Object, GetContextMock(null).Object);

            var item = await service.GetDepositCalculationCSVAsync(0);

            Assert.IsNull(item);
        }

        private Mock<IActionContextAccessor> GetContextMock(string userId)
        {
            ClaimsPrincipal user;
            if(userId != null)
            {
                user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) }));
            }
            else
            {
                user = new ClaimsPrincipal();
            }

            var context = new ActionContext();
            context.HttpContext = new DefaultHttpContext();
            context.HttpContext.User = user;

            var mock = new Mock<IActionContextAccessor>();
            mock.Setup(c => c.ActionContext).Returns(context);

            return mock;
        }
    }
}
