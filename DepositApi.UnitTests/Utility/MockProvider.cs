using DepositApi.Core.Enums;
using DepositApi.Core.Intrerfaces;
using DepositApi.Core.Models;
using Moq;
using System.Collections.Generic;

namespace DepositApi.UnitTests.Utility
{
    class MockProvider
    {
        private static DepositModel DepositModel
        {
            get
            {
                return new DepositModel { Amount = 1000, Percent = 12, Term = 3, UserId = "1", CalculationType = CalculationType.CompoundInterest };
            }
        }

        private static List<DepositModel> DepositModelsList
        {
            get
            {
                return new List<DepositModel>
                {
                    DepositModel,
                    new DepositModel { Amount = 2000, Percent = 4, Term = 4, UserId = "1", CalculationType = CalculationType.SimpleInterest},
                    new DepositModel { Amount = 3000, Percent = 3, Term = 5, UserId = "1", CalculationType = CalculationType.CompoundInterest}
                };
            }
        }

        private static List<DepositCalculationModel> DepositCalculationsList
        {
            get
            {
                return new List<DepositCalculationModel>
                {
                    new DepositCalculationModel { Month = 1, PercentAdded = 5, TotalAmount = 1005},
                    new DepositCalculationModel { Month = 2, PercentAdded = 5, TotalAmount = 1010},
                    new DepositCalculationModel { Month = 3, PercentAdded = 5, TotalAmount = 1015}
                };
            }
        }

        public static Mock<IDepositService> GetIDepositService()
        {
            var mock = new Mock<IDepositService>();
            mock.Setup(s => s.PercentCalculationAsync(It.IsAny<DepositModel>())).ReturnsAsync(DepositCalculationsList);
            mock.Setup(s => s.GetDepositsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(DepositModelsList);
            mock.Setup(s => s.GetDepositCalculationsAsync(It.IsAny<int>())).ReturnsAsync(DepositCalculationsList);
            mock.Setup(s => s.GetDepositCalculationCSVAsync(It.IsAny<int>())).ReturnsAsync(string.Empty);

            return mock;
        }
    }
}
