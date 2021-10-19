using DepositApi.Controllers;
using DepositApi.UnitTests.Utility;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DepositApi.UnitTests.ControllersTests
{
    [TestFixture]
    class GetCSVDepositApiControllerUnitTests
    {
        [Test]
        public async Task GetAsync_ProperMethodCall()
        {
            var mock = MockProvider.GetIDepositService();

            var controller = new GetCSVDepositApiController(mock.Object);
            var item = await controller.GetAsync(0);

            mock.Verify(s => s.GetDepositCalculationCSVAsync(It.IsAny<int>()));
        }
    }
}
