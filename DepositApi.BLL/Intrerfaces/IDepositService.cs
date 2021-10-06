using DepositApi.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepositApi.BLL.Intrerfaces
{
    public interface IDepositService
    {
        public Task<List<DepositCalculationDTO>> PercentCalculationAsync(DepositDTO deposit);

        public Task<List<DepositDTO>> GetDepositsAsync(int startIndex = 0, int count = 20);

        public Task<List<DepositCalculationDTO>> GetDepositCalculationsAsync(int depositId);

        public Task<byte[]> GetDepositCalculationCSVAsync(int depositId);
    }
}
