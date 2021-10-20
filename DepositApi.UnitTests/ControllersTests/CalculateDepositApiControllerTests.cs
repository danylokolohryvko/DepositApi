using DepositApi.UnitTests.Utility;
using NUnit.Framework;
using System.Threading.Tasks;
using DepositApi.Controllers;
using DepositApi.Core.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace DepositApi.UnitTests.ControllersTests
{
    [TestFixture]
    public class CalculateDepositApiControllerTests
    {
        [Test]
        public async Task GetAsync_ExpectListDepositCalculations_AsyncMethodCall()
        {
            var mock = MockProvider.GetIDepositService();

            var controller = new DepositCalculationApiController(mock.Object);
            var model = new DepositModel();
            var item = (OkObjectResult)await controller.GetAsync(model);
            

            Assert.AreEqual(item.Value, ModelsProvider.DepositCalculationsList);
            mock.Verify(s => s.PercentCalculationAsync(It.Is<DepositModel>(m => m == model)));
        }
    }
}
