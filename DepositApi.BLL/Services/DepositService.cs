using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DepositApi.Core.Enums;
using DepositApi.Core.Models;
using DepositApi.Core.Intrerfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace DepositApi.BLL.Services
{
    public class DepositService : IDepositService
    {
        private readonly IRepository<DepositModel> depositRepository;
        private readonly IRepository<DepositCalculationModel> depositCalculationRepository;
        private readonly IActionContextAccessor contextAccessor;
        private string UserId
        {
            get
            {
                var user = this.contextAccessor.ActionContext.HttpContext.User;
                return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        public DepositService(IRepository<DepositModel> depositRepository, IRepository<DepositCalculationModel> depositCalculationRepository, IActionContextAccessor contextAccessor)
        {
            this.depositRepository = depositRepository;
            this.depositCalculationRepository = depositCalculationRepository;
            this.contextAccessor = contextAccessor;
        }

        public async Task<List<DepositCalculationModel>> PercentCalculationAsync(DepositModel deposit)
        {
            deposit.Date = DateTime.UtcNow.Date;

            if (this.UserId != null)
            {
                deposit.UserId = this.UserId;
                await this.depositRepository.CreateAsync(deposit);
                deposit.Id = deposit.Id;
            }

            var result = deposit.CalculationType switch
            {
                CalculationType.CompoundInterest => this.CompoundInterestCalculation(deposit),
                _ => this.SimpleInterestCalculation(deposit)
            };

            if (this.UserId != null)
            {
                await this.depositCalculationRepository.CreateRangeAsync(result);
            }

            return result;
        }

        public async Task<List<DepositModel>> GetDepositsAsync(int startIndex = 0, int count = 20)
        {
            if (this.UserId == null)
            {
                return null;
            }

            var deposits = await this.depositRepository.FindRangeAsync(d => d.UserId == this.UserId, startIndex, count);

            return deposits;
        }

        public async Task<List<DepositCalculationModel>> GetDepositCalculationsAsync(int depositId)
        {
            var deposit = await this.depositRepository.FindAsync(depositId);

            if (deposit == null || deposit.UserId != this.UserId)
            {
                return null;
            }

            var depositCalculations = await this.depositCalculationRepository.FindRangeAsync(d => d.DepositId == depositId, 0, Int32.MaxValue);

            return depositCalculations;
        }

        public async Task<string> GetDepositCalculationCSVAsync(int depositId)
        {
            string result = string.Empty;

            var deposit = await this.depositRepository.FindAsync(depositId);

            if (deposit == null || deposit.UserId != this.UserId)
            {
                return null;
            }

            var depositCalculations = await this.depositCalculationRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            foreach (DepositCalculationModel depositCalculation in depositCalculations)
            {
                result += $"{depositCalculation.Month},{depositCalculation.PercentAdded},{depositCalculation.TotalAmount}\n";
            }

            return result;
        }

        private List<DepositCalculationModel> SimpleInterestCalculation(DepositModel deposit)
        {
            var result = new List<DepositCalculationModel>();

            for (int i = 1; i <= deposit.Term; i++)
            {
                result.Add(new DepositCalculationModel
                {
                    Month = i,
                    PercentAdded = Math.Round(deposit.Amount.Value * (deposit.Percent.Value / 12 / 100), 2, MidpointRounding.AwayFromZero),
                    TotalAmount = Math.Round(deposit.Amount.Value * (1 + deposit.Percent.Value / 12 / 100 * i), 2, MidpointRounding.AwayFromZero),
                    DepositId = deposit.Id
                });
            }

            return result;
        }

        private List<DepositCalculationModel> CompoundInterestCalculation(DepositModel depositDTO)
        {
            var result = new List<DepositCalculationModel>();

            decimal monthPercent = 1 + depositDTO.Percent.Value / 12 / 100;
            decimal thisMonthRate = monthPercent;
            decimal previousMonthRate = 1;

            for (int i = 1; i <= depositDTO.Term; i++)
            {
                result.Add(new DepositCalculationModel
                {
                    Month = i,
                    PercentAdded = Math.Round(depositDTO.Amount.Value * (thisMonthRate - previousMonthRate), 2, MidpointRounding.AwayFromZero),
                    TotalAmount = Math.Round(depositDTO.Amount.Value * thisMonthRate, 2, MidpointRounding.AwayFromZero),
                    DepositId = depositDTO.Id
                });
                previousMonthRate = thisMonthRate;
                thisMonthRate *= monthPercent;
            }

            return result;
        }
    }
}
