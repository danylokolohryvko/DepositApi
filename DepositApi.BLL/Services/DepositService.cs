using DepositApi.BLL.DTO;
using DepositApi.BLL.Intrerfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepositApi.BLL.Services
{
    public class DepositService : IDepositService
    {
        public DepositService() { }

        public List<DepositCalcDTO> PersentCalculationAsync(DepositDTO depositDTO)
        {
            var result = new List<DepositCalcDTO>();
            for(int i = 1; i <= depositDTO.Term; i ++)
            {
                result.Add(new DepositCalcDTO
                {
                    Month = i,
                    PercentAdded = Math.Round(depositDTO.Amount * (depositDTO.Percent / 12 / 100), 2, MidpointRounding.AwayFromZero),
                    TotalAmount = Math.Round(depositDTO.Amount * (1 + depositDTO.Percent / 12 / 100 * i), 2, MidpointRounding.AwayFromZero),
                });
            }
            return result;
        }
    }
}
