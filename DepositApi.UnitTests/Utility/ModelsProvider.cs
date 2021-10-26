using DepositApi.Core.Enums;
using DepositApi.Core.Models;
using System.Collections.Generic;

namespace DepositApi.UnitTests.Utility
{
    public class ModelsProvider
    {
        public static DepositModel DepositModel = new DepositModel { Amount = 1000, Percent = 12, Term = 3, UserId = "1", CalculationType = CalculationType.CompoundInterest };

        public static List<DepositModel> DepositModelsList = new List<DepositModel>
        {
            DepositModel,
            new DepositModel { Amount = 2000, Percent = 4, Term = 4, UserId = "1", CalculationType = CalculationType.SimpleInterest},
            new DepositModel { Amount = 3000, Percent = 3, Term = 5, UserId = "1", CalculationType = CalculationType.CompoundInterest}
        };

        public static List<DepositCalculationModel> DepositCalculationsList = new List<DepositCalculationModel>
        {
            new DepositCalculationModel { Month = 1, PercentAdded = 5, TotalAmount = 1005},
            new DepositCalculationModel { Month = 2, PercentAdded = 5, TotalAmount = 1010},
            new DepositCalculationModel { Month = 3, PercentAdded = 5, TotalAmount = 1015}
        };

    }
}
