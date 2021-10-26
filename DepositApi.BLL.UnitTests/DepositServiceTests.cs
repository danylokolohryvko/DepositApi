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
    public class DepositServiceTests
    {
        private static DepositModel DepositModel = new DepositModel { Amount = 1000, Percent = 12, Term = 3, UserId = "1", CalculationType = CalculationType.CompoundInterest};

        private static List<DepositModel> DepositModelsList = new List<DepositModel>
        {
            DepositModel,
            new DepositModel { Amount = 2000, Percent = 4, Term = 4, UserId = "1", CalculationType = CalculationType.SimpleInterest},
            new DepositModel { Amount = 3000, Percent = 3, Term = 5, UserId = "1", CalculationType = CalculationType.CompoundInterest}
        };

        private static List<DepositCalculationModel> DepositCalculationsList = new List<DepositCalculationModel>
        {
            new DepositCalculationModel { Month = 1, PercentAdded = 5, TotalAmount = 1005},
            new DepositCalculationModel { Month = 2, PercentAdded = 5, TotalAmount = 1010},
            new DepositCalculationModel { Month = 3, PercentAdded = 5, TotalAmount = 1015}
        };

        private Mock<IRepository<DepositModel>> DepositRepositoryMock;

        private Mock<IRepository<DepositCalculationModel>> DepositCalculationRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            DepositRepositoryMock = new Mock<IRepository<DepositModel>>();
            DepositRepositoryMock.Setup(r => r.FindAsync(It.IsAny<int>())).ReturnsAsync(DepositModel);
            DepositRepositoryMock.Setup(r => r.FindRangeAsync(
                It.IsAny<Expression<Func<DepositModel, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(DepositModelsList);

            DepositCalculationRepositoryMock = new Mock<IRepository<DepositCalculationModel>>();
            DepositCalculationRepositoryMock.Setup(r => r.FindRangeAsync(
                It.IsAny<Expression<Func<DepositCalculationModel, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(DepositCalculationsList);
        }

        [Test]
        public async Task PercentCalculationAsync_SimpleInterest_ExpectListDepositCalculation_AsyncMethodCall()
        {
            var contextMock = GetContextMock("1");
            var service = new DepositService(this.DepositRepositoryMock.Object, this.DepositCalculationRepositoryMock.Object, contextMock.Object);
            var model = DepositModel;
            model.CalculationType = CalculationType.SimpleInterest;

            var item = await service.PercentCalculationAsync(model);

            Assert.AreEqual(10, item[0].PercentAdded);
            Assert.AreEqual(1010, item[0].TotalAmount);

            Assert.AreEqual(10, item[1].PercentAdded);
            Assert.AreEqual(1020, item[1].TotalAmount);

            Assert.AreEqual(10, item[2].PercentAdded);
            Assert.AreEqual(1030, item[2].TotalAmount);

            this.DepositRepositoryMock.Verify(r => r.CreateAsync(It.Is<DepositModel>(m => m == model)));
            this.DepositCalculationRepositoryMock.Verify(r => r.CreateRangeAsync(It.Is<List<DepositCalculationModel>>(m => m ==item)));
        }

        [Test]
        public async Task PercentCalculationAsync_CompoundInterest_ExpectListDepositCalculation_AsyncMethodCall()
        {
            var contextMock = GetContextMock("1");

            var service = new DepositService(this.DepositRepositoryMock.Object, this.DepositCalculationRepositoryMock.Object, contextMock.Object);
            var model = DepositModel;
            var item = await service.PercentCalculationAsync(model);

            Assert.AreEqual(10, item[0].PercentAdded);
            Assert.AreEqual(1010, item[0].TotalAmount);

            Assert.AreEqual(10.1, item[1].PercentAdded);
            Assert.AreEqual(1020.1, item[1].TotalAmount);

            Assert.AreEqual(10.2, item[2].PercentAdded);
            Assert.AreEqual(1030.3, item[2].TotalAmount);

            this.DepositRepositoryMock.Verify(r => r.CreateAsync(It.Is<DepositModel>(m => m == model)));
            this.DepositCalculationRepositoryMock.Verify(r => r.CreateRangeAsync(It.Is<List<DepositCalculationModel>>(m => m == item)));
        }

        [Test]
        public async Task GetDepositsAsync_WithUserId_ExpectListDeposit_AsyncMethodCall()
        {
            var contextMock = GetContextMock("1");
            var service = new DepositService(this.DepositRepositoryMock.Object, this.DepositCalculationRepositoryMock.Object, contextMock.Object);

            var item = await service.GetDepositsAsync(0, 3);

            Assert.AreEqual(DepositModelsList, item);
            this.DepositRepositoryMock.Verify(r => r.FindRangeAsync(It.IsAny<Expression<Func<DepositModel, bool>>>(), It.Is<int>(i => i == 0), It.Is<int>(i => i == 3)));
        }

        [Test]
        public async Task GetDepositsAsync_WithoutUserId_ExpectNull_AsyncMethodCall()
        {
            var service = new DepositService(this.DepositRepositoryMock.Object, this.DepositCalculationRepositoryMock.Object, GetContextMock(null).Object);

            var item = await service.GetDepositsAsync(0, 3);

            Assert.IsNull(item);
        }

        [Test]
        public async Task GetDepositCalculationsAsync_WithUserId_ExpectListDepositCalculation_AsyncMethodCall()
        {
            var contextMock = GetContextMock("1");
            var service = new DepositService(this.DepositRepositoryMock.Object, this.DepositCalculationRepositoryMock.Object, contextMock.Object);

            var item = await service.GetDepositCalculationsAsync(0);

            Assert.AreEqual(DepositCalculationsList ,item);
            this.DepositRepositoryMock.Verify(r => r.FindAsync(It.Is<int>(i => i == 0)));
            this.DepositCalculationRepositoryMock.Verify(r => r.FindRangeAsync(It.IsAny<Expression<Func<DepositCalculationModel, bool>>>(), It.Is<int>(i => i == 0), It.Is<int>(i => i == Int32.MaxValue)));
        }

        [Test]
        public async Task GetDepositCalculationsAsync_WithoutUserId_ExpectNull_AsyncMethodCall()
        {
            var service = new DepositService(this.DepositRepositoryMock.Object, this.DepositCalculationRepositoryMock.Object, GetContextMock(null).Object);

            var item = await service.GetDepositCalculationsAsync(0);

            Assert.IsNull(item);
        }

        [Test]
        public async Task GetDepositCalculationCSVAsync_WithUserId_ExpectString_AsyncMethodCall()
        {
            var contextMock = GetContextMock("1");
            var service = new DepositService(this.DepositRepositoryMock.Object, this.DepositCalculationRepositoryMock.Object, contextMock.Object);

            var item = await service.GetDepositCalculationCSVAsync(0);

            Assert.AreEqual("1,5,1005\n2,5,1010\n3,5,1015\n", item);
            this.DepositRepositoryMock.Verify(r => r.FindAsync(It.IsAny<int>()));
            this.DepositCalculationRepositoryMock.Verify(r => r.FindRangeAsync(It.IsAny<Expression<Func<DepositCalculationModel, bool>>>(), It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public async Task GetDepositCalculationCSVAsync_WithoutUserId_ExpectNull_AsyncMethodCall()
        {
            var service = new DepositService(this.DepositRepositoryMock.Object, this.DepositCalculationRepositoryMock.Object, GetContextMock(null).Object);

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
