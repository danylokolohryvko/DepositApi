using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.BLL.Intrerfaces;
using DepositApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepositApi.Controllers
{
    [ApiController]
    [Route("/api/calculate")]
    public class DepositCalcApiController : ControllerBase
    {
        private readonly IDepositService depositService;
        private readonly IMapper mapper;

        public DepositCalcApiController(IDepositService depositService, IMapper mapper)
        {
            this.depositService = depositService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Get(DepositModel deposit)
        {
            if (ModelState.IsValid)
            {
                var depositDTO = this.mapper.Map<DepositDTO>(deposit);
                var depositCalcDTO = this.depositService.PersentCalculationAsync(depositDTO);
                var depositCalc = this.mapper.Map<List<DepositCalcModel>>(depositCalcDTO);
                return new JsonResult(depositCalc);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
