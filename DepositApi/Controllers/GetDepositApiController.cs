using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.BLL.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public async Task<ActionResult> Get(GetDepositsModel model)
        {
            var depositDTOs = await this.depositService.GetDepositsAsync(model.StartIndex, model.Count) ;
            var deposits = this.mapper.Map<List<DepositModel>>(depositDTOs);
            return new JsonResult(deposits);
        }
    }
}
