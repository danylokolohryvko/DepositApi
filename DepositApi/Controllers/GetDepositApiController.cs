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
    [Route("/api/deposit")]
    public class GetDepositApiController : ControllerBase
    {
        private readonly IDepositService depositService;
        private readonly IMapper mapper;
        
        public GetDepositApiController(IDepositService depositService, IMapper mapper)
        {
            this.depositService = depositService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAsync([FromQuery] DepositsViewModel model)
        {
            string id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var depositDTOs = await this.depositService.GetDepositsAsync(model.StartIndex.Value, model.Count.Value, id) ;
            var deposits = this.mapper.Map<List<DepositModel>>(depositDTOs);

            return Ok(deposits);
        }
    }
}
