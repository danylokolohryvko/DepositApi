using DepositApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepositApi.Core.Intrerfaces
{
    public interface IDepositService
    {
        public Task<List<DepositCalculationModel>> PercentCalculationAsync(DepositModel deposit);

        public Task<List<DepositModel>> GetDepositsAsync(int startIndex = 0, int count = 20);

        public Task<List<DepositCalculationModel>> GetDepositCalculationsAsync(int depositId);

        public Task<string> GetDepositCalculationCSVAsync(int depositId);
    }
}
