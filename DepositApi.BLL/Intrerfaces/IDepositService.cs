using DepositApi.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DepositApi.BLL.Intrerfaces
{
    public interface IDepositService
    {
        public List<DepositCalcDTO> PersentCalculationAsync(DepositDTO deposit);
    }
}
