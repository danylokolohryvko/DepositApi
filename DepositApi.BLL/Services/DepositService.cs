using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.BLL.Intrerfaces;
using DepositApi.DAL.Models;
using DepositApi.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<DepositCalculationDTO>> PercentCalculationAsync(DepositDTO depositDTO)
        {
            var result = new List<DepositCalculationDTO>();
            depositDTO.Date = DateTime.UtcNow.Date;
            var deposit = this.mapper.Map<Deposit>(depositDTO);
            await this.depositRepository.CreateAsync(deposit);
            for (int i = 1; i <= depositDTO.Term; i ++)
            {
                result.Add(new DepositCalculationDTO
                {
                    Month = i,
                    PercentAdded = Math.Round(depositDTO.Amount * (depositDTO.Percent / 12 / 100), 2, MidpointRounding.AwayFromZero),
                    TotalAmount = Math.Round(depositDTO.Amount * (1 + depositDTO.Percent / 12 / 100 * i), 2, MidpointRounding.AwayFromZero),
                    DepositId = deposit.Id
                });
            }
            var depositCalculations = mapper.Map<List<DepositCalculation>>(result);
            await this.depositCalculationRepository.CreateRangeAsync(depositCalculations);

            return result;
        }

        public async Task<List<DepositDTO>> GetDepositsAsync(int startIndex = 0, int count = 20)
        {
            var deposits = await this.depositRepository.FindRangeAsync(d => true, startIndex, count);
            var depositsDTO = this.mapper.Map<List<DepositDTO>>(deposits);

            return depositsDTO;
        }

        public async Task<List<DepositCalculationDTO>> GetDepositCalculationsAsync(int depositId)
        {
            var depositCalculations = await this.depositCalculationRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            var depositCalculationDTOs = this.mapper.Map<List<DepositCalculationDTO>>(depositCalculations);

            return depositCalculationDTOs;
        }

        public async Task<byte[]> GetDepositCalculationCSVAsync(int depositId)
        {
            string result = string.Empty;
            var depositCalculations = await this.depositCalculationRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            foreach (DepositCalculation depositCalculation in depositCalculations)
            {
                result += $"{depositCalculation.Month},{depositCalculation.PercentAdded},{depositCalculation.TotalAmount}\n";
            }

            return Encoding.ASCII.GetBytes(result);
        }
    }
}
