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
    class GetDepositApiControllerUnitTests
    {
        [Test]
        public async Task GetAsync_ProperMethodCall()
        {
            var mock = MockProvider.GetIDepositService();
            var model = new DepositsViewModel
            {
                Count = 1,
                StartIndex = 1
            };

            var controller = new GetDepositApiController(mock.Object);
            var item = await controller.GetAsync(model);

            mock.Verify(s => s.GetDepositsAsync(It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}
