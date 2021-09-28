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
        private readonly IRepository<DepositCalc> depositCalcRepository;
        private readonly IMapper mapper;

        public DepositService(IRepository<Deposit> depositRepository, IRepository<DepositCalc> depositCalcRepository, IMapper mapper)
        {
            this.depositRepository = depositRepository;
            this.depositCalcRepository = depositCalcRepository;
            this.mapper = mapper;
        }

        public async Task<List<DepositCalcDTO>> PersentCalculationAsync(DepositDTO depositDTO)
        {
            var result = new List<DepositCalcDTO>();
            depositDTO.Date = DateTime.UtcNow.Date;
            var deposit = this.mapper.Map<Deposit>(depositDTO);
            await this.depositRepository.CreateAsync(deposit);
            for (int i = 1; i <= depositDTO.Term; i ++)
            {
                result.Add(new DepositCalcDTO
                {
                    Month = i,
                    PercentAdded = Math.Round(depositDTO.Amount * (depositDTO.Percent / 12 / 100), 2, MidpointRounding.AwayFromZero),
                    TotalAmount = Math.Round(depositDTO.Amount * (1 + depositDTO.Percent / 12 / 100 * i), 2, MidpointRounding.AwayFromZero),
                    DepositId = deposit.Id
                });
            }
            var depositCalcs = mapper.Map<List<DepositCalc>>(result);
            await this.depositCalcRepository.CreateRangeAsync(depositCalcs);
            return result;
        }

        public async Task<List<DepositDTO>> GetDepositsAsync(int startIndex = 0, int count = 20)
        {
            var deposits = await this.depositRepository.FindRangeAsync(d => true, startIndex, count);
            var depositsDTO = this.mapper.Map<List<DepositDTO>>(deposits);
            return depositsDTO;
        }

        public async Task<List<DepositCalcDTO>> GetDepositCalcsAsync(int depositId)
        {
            var depositCalcs = await this.depositCalcRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            var depositCalcDTOs = this.mapper.Map<List<DepositCalcDTO>>(depositCalcs);
            return depositCalcDTOs;
        }

        public async Task<byte[]> GetDepositCalcCSVAsync(int depositId)
        {
            string result = string.Empty;
            var depositCalcs = await this.depositCalcRepository.FindRangeAsync(d => d.DepositId == depositId, 0, 100);
            foreach (DepositCalc depositCalc in depositCalcs)
            {
                result += $"{depositCalc.Month},{depositCalc.PercentAdded},{depositCalc.TotalAmount}\n";
            }
            return Encoding.ASCII.GetBytes(result);
        }
    }
}
