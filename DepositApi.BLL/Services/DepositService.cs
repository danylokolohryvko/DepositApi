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
        private readonly IRepository<DepositCalculation> depositCalculetionRepository;
        private readonly IMapper mapper;

        public DepositService(IRepository<Deposit> depositRepository, IRepository<DepositCalculation> depositCalculetionRepository, IMapper mapper)
        {
            this.depositRepository = depositRepository;
            this.depositCalculetionRepository = depositCalculetionRepository;
            this.mapper = mapper;
        }

        public async Task<List<DepositCalculationDTO>> PersentCalculationAsync(DepositDTO depositDTO)
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
            var depositCalculetions = mapper.Map<List<DepositCalculation>>(result);
            await this.depositCalculetionRepository.CreateRangeAsync(depositCalculetions);

            return result;
        }

        public async Task<List<DepositDTO>> GetDepositsAsync(int startIndex = 0, int count = 20)
        {
            var deposits = await this.depositRepository.FindRangeAsync(d => true, startIndex, count);
            var depositsDTO = this.mapper.Map<List<DepositDTO>>(deposits);

            return depositsDTO;
        }

        public async Task<List<DepositCalculationDTO>> GetDepositCalculetionsAsync(int depositId)
        {
            var depositCalculetions = await this.depositCalculetionRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            var depositCalculetionDTOs = this.mapper.Map<List<DepositCalculationDTO>>(depositCalculetions);

            return depositCalculetionDTOs;
        }

        public async Task<byte[]> GetDepositCalculetionCSVAsync(int depositId)
        {
            string result = string.Empty;
            var depositCalculetions = await this.depositCalculetionRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            foreach (DepositCalculation depositCalculetion in depositCalculetions)
            {
                result += $"{depositCalculetion.Month},{depositCalculetion.PercentAdded},{depositCalculetion.TotalAmount}\n";
            }

            return Encoding.ASCII.GetBytes(result);
        }
    }
}
