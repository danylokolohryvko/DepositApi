using DepositApi.Core.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DepositApi.Controllers
{
    [ApiController]
    [Route("/api/deposit")]
    public class GetDepositApiController : ControllerBase
    {
        private readonly IDepositService depositService;
        
        public GetDepositApiController(IDepositService depositService)
        {
            this.depositService = depositService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAsync([FromQuery] DepositsViewModel model)
        {
            string id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var deposits = await this.depositService.GetDepositsAsync(model.StartIndex.Value, model.Count.Value, id) ;

            return Ok(deposits);
        }
    }
}
