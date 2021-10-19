using DepositApi.Controllers;
using DepositApi.Models;
using DepositApi.UnitTests.Utility;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepositApi.UnitTests.ControllersTests
{
    [TestFixture]
    class GetDepositCalculationApiControllerUnitTests
    {
        [Test]
        public async Task GetAsync_ProperMethodCall()
        {
            var mock = MockProvider.GetIDepositService();
            var model = new DepositCalculationsViewModel
            {
                DepositId = 0
            };

            var controller = new GetDepositCalculationApiController(mock.Object);
            var item = await controller.GetAsync(model);

            mock.Verify(s => s.GetDepositCalculationsAsync(It.IsAny<int>()));
        }
    }
}
