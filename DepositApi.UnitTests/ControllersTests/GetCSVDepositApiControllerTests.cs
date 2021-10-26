using DepositApi.Controllers;
using DepositApi.UnitTests.Utility;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DepositApi.UnitTests.ControllersTests
{
    [TestFixture]
    public class GetCSVDepositApiControllerTests
    {
        [Test]
        public async Task GetAsync_ExpectString_AsyncMethodCall()
        {
            var mock = MockProvider.GetIDepositService();

            var controller = new GetCSVDepositApiController(mock.Object);
            var item = (OkObjectResult)await controller.GetAsync(0);

            Assert.AreEqual(item.Value, string.Empty);
            mock.Verify(s => s.GetDepositCalculationCSVAsync(It.Is<int>(i => i == 0)));
        }
    }
}
