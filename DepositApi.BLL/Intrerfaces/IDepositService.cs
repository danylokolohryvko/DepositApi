using DepositApi.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepositApi.BLL.Intrerfaces
{
    public interface IDepositService
    {
        public Task<List<DepositCalcDTO>> PersentCalculationAsync(DepositDTO deposit);

        public Task<List<DepositDTO>> GetDepositsAsync(int startIndex = 0, int count = 20);

        public Task<List<DepositCalcDTO>> GetDepositCalcsAsync(int depositId);

        public Task<byte[]> GetDepositCalcCSVAsync(int depositId);
    }
}
