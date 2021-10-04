using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.BLL.Intrerfaces;
using DepositApi.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepositApi.Controllers
{
    [ApiController]
    [Route("/api/calculate")]
    public class DepositCalculationApiController : ControllerBase
    {
        private readonly IDepositService depositService;
        private readonly IMapper mapper;

        public DepositCalculationApiController(IDepositService depositService, IMapper mapper)
        {
            this.depositService = depositService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get(DepositModel deposit)
        {
            var depositDTO = this.mapper.Map<DepositDTO>(deposit);
            var depositCalculationDTO = await this.depositService.PercentCalculationAsync(depositDTO);
            var depositCalculation = this.mapper.Map<List<DepositCalculationModel>>(depositCalculationDTO);

            return Ok(depositCalculation);
        }
    }
}
