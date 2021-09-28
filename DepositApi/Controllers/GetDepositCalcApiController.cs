using AutoMapper;
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
    [Route("/api/depositcalc")]
    public class GetDepositCalcApiController : ControllerBase
    {
        private readonly IDepositService depositService;
        private readonly IMapper mapper;

        public GetDepositCalcApiController(IDepositService depositService, IMapper mapper)
        {
            this.depositService = depositService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get(GetDepositCalcsModel model)
        {
            var depositCalcDTOs = await this.depositService.GetDepositCalcsAsync(model.DepositId);
            var depositCalcs = this.mapper.Map<List<DepositCalcModel>>(depositCalcDTOs);
            return new JsonResult(depositCalcs);
        }
    }
}
