using AutoMapper;
using DepositApi.BLL.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult> Get([FromQuery] GetDepositsModel model)
        {
            var depositDTOs = await this.depositService.GetDepositsAsync(model.StartIndex.Value, model.Count.Value) ;
            var deposits = this.mapper.Map<List<DepositModel>>(depositDTOs);

            return Ok(deposits);
        }
    }
}
