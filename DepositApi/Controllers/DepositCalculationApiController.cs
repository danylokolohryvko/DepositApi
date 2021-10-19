using DepositApi.Core.Intrerfaces;
using DepositApi.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DepositApi.Controllers
{
    [ApiController]
    [Route("/api/calculate")]
    public class DepositCalculationApiController : ControllerBase
    {
        private readonly IDepositService depositService;

        public DepositCalculationApiController(IDepositService depositService)
        {
            this.depositService = depositService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] DepositModel deposit)
        {
            var depositCalculation = await this.depositService.PercentCalculationAsync(deposit);

            return Ok(depositCalculation);
        }
    }
}
