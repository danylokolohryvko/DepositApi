using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.BLL.Intrerfaces;
using DepositApi.DAL.Models;
using DepositApi.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DepositApi.Core.Enums;

namespace DepositApi.BLL.Services
{
    public class DepositService : IDepositService
    {
        private readonly IRepository<Deposit> depositRepository;
        private readonly IRepository<DepositCalculation> depositCalculationRepository;
        private readonly IMapper mapper;

        public DepositService(IRepository<Deposit> depositRepository, IRepository<DepositCalculation> depositCalculationRepository, IMapper mapper)
        {
            this.depositRepository = depositRepository;
            this.depositCalculationRepository = depositCalculationRepository;
            this.mapper = mapper;
        }

        public async Task<List<DepositCalculationDTO>> PercentCalculationAsync(DepositDTO depositDTO, string userId = null)
        {
            depositDTO.Date = DateTime.UtcNow.Date;
            var deposit = this.mapper.Map<Deposit>(depositDTO);

            if (userId != null)
            {
                deposit.UserId = userId;
                await this.depositRepository.CreateAsync(deposit);
                depositDTO.Id = deposit.Id;
            }

            var result = depositDTO.CalculationType switch
            {
                CalculationType.CompoundInterest => this.CompoundInterestCalculation(depositDTO),
                _ => this.SimpleInterestCalculation(depositDTO)
            };

            var depositCalculations = mapper.Map<List<DepositCalculation>>(result);

            if (userId != null)
            {
                await this.depositCalculationRepository.CreateRangeAsync(depositCalculations);
            }

            return result;
        }

        public async Task<List<DepositDTO>> GetDepositsAsync(int startIndex = 0, int count = 20, string userId = null)
        {
            if (userId == null)
            {
                return null;
            }

            var deposits = await this.depositRepository.FindRangeAsync(d => d.UserId == userId, startIndex, count);
            var depositsDTO = this.mapper.Map<List<DepositDTO>>(deposits);

            return depositsDTO;
        }

        public async Task<List<DepositCalculationDTO>> GetDepositCalculationsAsync(int depositId, string userId)
        {
            var deposit = await this.depositRepository.FindAsync(depositId);

            if (deposit == null || deposit.UserId != userId)
            {
                return null;
            }

            var depositCalculations = await this.depositCalculationRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            var depositCalculationDTOs = this.mapper.Map<List<DepositCalculationDTO>>(depositCalculations);

            return depositCalculationDTOs;
        }

        public async Task<string> GetDepositCalculationCSVAsync(int depositId, string userId)
        {
            string result = string.Empty;

            var deposit = await this.depositRepository.FindAsync(depositId);

            if (deposit == null || deposit.UserId != userId)
            {
                return null;
            }

            var depositCalculations = await this.depositCalculationRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            foreach (DepositCalculation depositCalculation in depositCalculations)
            {
                result += $"{depositCalculation.Month},{depositCalculation.PercentAdded},{depositCalculation.TotalAmount}\n";
            }

            return result;
        }

        private List<DepositCalculationDTO> SimpleInterestCalculation(DepositDTO depositDTO)
        {
            var result = new List<DepositCalculationDTO>();

            for (int i = 1; i <= depositDTO.Term; i++)
            {
                result.Add(new DepositCalculationDTO
                {
                    Month = i,
                    PercentAdded = Math.Round(depositDTO.Amount * (depositDTO.Percent / 12 / 100), 2, MidpointRounding.AwayFromZero),
                    TotalAmount = Math.Round(depositDTO.Amount * (1 + depositDTO.Percent / 12 / 100 * i), 2, MidpointRounding.AwayFromZero),
                    DepositId = depositDTO.Id
                });
            }

            return result;
        }

        private List<DepositCalculationDTO> CompoundInterestCalculation(DepositDTO depositDTO)
        {
            var result = new List<DepositCalculationDTO>();

            decimal monthPercent = 1 + depositDTO.Percent / 12 / 100;
            decimal thisMonthRate = monthPercent;
            decimal previousMonthRate = 1;

            for (int i = 1; i <= depositDTO.Term; i++)
            {
                result.Add(new DepositCalculationDTO
                {
                    Month = i,
                    PercentAdded = Math.Round(depositDTO.Amount * (thisMonthRate - previousMonthRate), 2, MidpointRounding.AwayFromZero),
                    TotalAmount = Math.Round(depositDTO.Amount * thisMonthRate, 2, MidpointRounding.AwayFromZero),
                    DepositId = depositDTO.Id
                });
                previousMonthRate = thisMonthRate;
                thisMonthRate *= monthPercent;
            }

            return result;
        }
    }
}
