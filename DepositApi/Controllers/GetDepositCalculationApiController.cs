using DepositApi.Core.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            string id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var depositCalculations = await this.depositService.GetDepositCalculationsAsync(model.DepositId.Value, id);

            return Ok(depositCalculations);
        }
    }
}
