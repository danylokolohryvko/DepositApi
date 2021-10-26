using DepositApi.Core.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DepositApi.Controllers
{
    [ApiController]
    [Route("/api/depositcalculation")]
    public class GetDepositCalculationApiController : ControllerBase
    {
        private readonly IDepositService depositService;

        public GetDepositCalculationApiController(IDepositService depositService)
        {
            this.depositService = depositService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAsync([FromQuery] DepositCalculationsViewModel model)
        {
            var depositCalculations = await this.depositService.GetDepositCalculationsAsync(model.DepositId.Value);

            return Ok(depositCalculations);
        }
    }
}
