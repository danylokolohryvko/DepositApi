using AutoMapper;
using DepositApi.BLL.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult> Get(GetDepositCalculationsModel model)
        {
            var depositCalculationDTOs = await this.depositService.GetDepositCalculationsAsync(model.DepositId.Value);
            var depositCalculations = this.mapper.Map<List<DepositCalculationModel>>(depositCalculationDTOs);

            return Ok(depositCalculations);
        }
    }
}
