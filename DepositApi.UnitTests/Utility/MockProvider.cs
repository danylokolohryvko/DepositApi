using DepositApi.Core.Enums;
using DepositApi.Core.Intrerfaces;
using DepositApi.Core.Models;
using Moq;
using System.Collections.Generic;

namespace DepositApi.UnitTests.Utility
{
    public class MockProvider
    {
        public static Mock<IDepositService> GetIDepositService()
        {
            var mock = new Mock<IDepositService>();
            mock.Setup(s => s.PercentCalculationAsync(It.IsAny<DepositModel>())).ReturnsAsync(ModelsProvider.DepositCalculationsList);
            mock.Setup(s => s.GetDepositsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(ModelsProvider.DepositModelsList);
            mock.Setup(s => s.GetDepositCalculationsAsync(It.IsAny<int>())).ReturnsAsync(ModelsProvider.DepositCalculationsList);
            mock.Setup(s => s.GetDepositCalculationCSVAsync(It.IsAny<int>())).ReturnsAsync(string.Empty);

            return mock;
        }
    }
}
