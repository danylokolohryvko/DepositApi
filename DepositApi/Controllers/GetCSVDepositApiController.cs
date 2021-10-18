using DepositApi.BLL.Intrerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            string id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var depositCalculationCSV = await this.depositService.GetDepositCalculationCSVAsync(depositId, id);

            return Ok(depositCalculationCSV);
        }
    }
}
