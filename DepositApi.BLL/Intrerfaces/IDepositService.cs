using DepositApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepositApi.BLL.Intrerfaces
{
    public interface IDepositService
    {
        public Task<List<DepositCalculationModel>> PercentCalculationAsync(DepositModel deposit, string userId = null);

        public Task<List<DepositModel>> GetDepositsAsync(int startIndex = 0, int count = 20, string userId = null);

        public Task<List<DepositCalculationModel>> GetDepositCalculationsAsync(int depositId, string userId);

        public Task<string> GetDepositCalculationCSVAsync(int depositId, string userId);
    }
}
