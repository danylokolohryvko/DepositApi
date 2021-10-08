﻿using DepositApi.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepositApi.BLL.Intrerfaces
{
    public interface IDepositService
    {
        public Task<List<DepositCalculationDTO>> PercentCalculationAsync(DepositDTO deposit, string userId = null);

        public Task<List<DepositDTO>> GetDepositsAsync(int startIndex = 0, int count = 20, string userId = null);

        public Task<List<DepositCalculationDTO>> GetDepositCalculationsAsync(int depositId, string userId);

        public Task<string> GetDepositCalculationCSVAsync(int depositId, string userId);
    }
}
