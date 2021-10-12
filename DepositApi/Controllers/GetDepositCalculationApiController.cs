using AutoMapper;
using DepositApi.BLL.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DepositApi.Controllers
{
    [ApiController]
    [Route("/api/depositcalculation")]
    public class GetDepositCalculationApiController : ControllerBase
    {
        private readonly IDepositService depositService;
        private readonly IMapper mapper;

        public GetDepositCalculationApiController(IDepositService depositService, IMapper mapper)
        {
            this.depositService = depositService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get([FromQuery] DepositCalculationsViewModel model)
        {
            string id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var depositCalculationDTOs = await this.depositService.GetDepositCalculationsAsync(model.DepositId.Value, id);
            var depositCalculations = this.mapper.Map<List<DepositCalculationModel>>(depositCalculationDTOs);

            return Ok(depositCalculations);
        }
    }
}
