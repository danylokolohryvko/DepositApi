using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.BLL.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
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
        public async Task<ActionResult> GetAsync([FromQuery] DepositModel deposit)
        {
            string id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var depositDTO = this.mapper.Map<DepositDTO>(deposit);
            var depositCalculationDTO = await this.depositService.PercentCalculationAsync(depositDTO, id);
            var depositCalculation = this.mapper.Map<List<DepositCalculationModel>>(depositCalculationDTO);

            return Ok(depositCalculation);
        }
    }
}
