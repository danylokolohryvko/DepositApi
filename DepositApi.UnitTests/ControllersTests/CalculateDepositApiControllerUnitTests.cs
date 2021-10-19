using DepositApi.UnitTests.Utility;
using NUnit.Framework;
using System.Threading.Tasks;
using DepositApi.Controllers;
using DepositApi.Core.Models;
using Moq;

namespace DepositApi.UnitTests.ControllersTests
{
    [TestFixture]
    class CalculateDepositApiControllerUnitTests
    {
        [Test]
        public async Task GetAsync_ProperMethodCall()
        {
            var mock = MockProvider.GetIDepositService();

            var controller = new DepositCalculationApiController(mock.Object);
            var item = await controller.GetAsync(new DepositModel());

            mock.Verify(s => s.PercentCalculationAsync(It.IsAny<DepositModel>()));
        }
    }
}
