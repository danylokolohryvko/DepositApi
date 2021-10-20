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
    public class GetDepositCalculationApiControllerTests
    {
        [Test]
        public async Task GetAsync_ExpectListDepositCalculation_AsyncMethodCall()
        {
            var mock = MockProvider.GetIDepositService();
            var model = new DepositCalculationsViewModel
            {
                DepositId = 0
            };

            var controller = new GetDepositCalculationApiController(mock.Object);
            var item = (OkObjectResult)await controller.GetAsync(model);

            Assert.AreEqual(item.Value, ModelsProvider.DepositCalculationsList);
            mock.Verify(s => s.GetDepositCalculationsAsync(It.Is<int>(i => i == 0)));
        }
    }
}
