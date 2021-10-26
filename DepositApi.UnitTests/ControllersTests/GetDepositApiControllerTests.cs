using DepositApi.Controllers;
using DepositApi.Models;
using DepositApi.UnitTests.Utility;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepositApi.UnitTests.ControllersTests
{
    [TestFixture]
    public class GetDepositApiControllerTests
    {
        [Test]
        public async Task GetAsync_ExpectListDeposit_AsyncMethodCall()
        {
            var mock = MockProvider.GetIDepositService();
            var model = new DepositsViewModel
            {
                Count = 1,
                StartIndex = 1
            };

            var controller = new GetDepositApiController(mock.Object);
            var item = (OkObjectResult)await controller.GetAsync(model);

            Assert.AreEqual(item.Value, ModelsProvider.DepositModelsList);
            mock.Verify(s => s.GetDepositsAsync(It.Is<int>(i => i == 1), It.Is<int>(i => i == 1)));
        }
    }
}
