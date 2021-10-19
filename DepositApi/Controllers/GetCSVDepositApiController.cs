using DepositApi.Core.Intrerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DepositApi.Controllers
{
    [ApiController]
    [Route("/api/depositcalculation/csv")]
    public class GetCSVDepositApiController : ControllerBase
    {
        private readonly IDepositService depositService;

        public GetCSVDepositApiController(IDepositService depositService)
        {
            this.depositService = depositService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAsync(int depositId)
        {
            var depositCalculationCSV = await this.depositService.GetDepositCalculationCSVAsync(depositId);

            return Ok(depositCalculationCSV);
        }
    }
}
