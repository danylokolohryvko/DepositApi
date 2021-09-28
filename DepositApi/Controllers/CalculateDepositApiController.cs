using AutoMapper;
using DepositApi.BLL.DTO;
using DepositApi.BLL.Intrerfaces;
using DepositApi.Models;
using FluentValidation.Results;
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
        public async Task<ActionResult> Get(DepositModel deposit)
        {
            var validator = new DepositValidator();
            var valResult = validator.Validate(deposit);
            if (valResult.IsValid)
            {
                var depositDTO = this.mapper.Map<DepositDTO>(deposit);
                var depositCalcDTO = await this.depositService.PersentCalculationAsync(depositDTO);
                var depositCalc = this.mapper.Map<List<DepositCalcModel>>(depositCalcDTO);
                return new JsonResult(depositCalc);
            }
            else
            {
                foreach (ValidationFailure f in valResult.Errors)
                {
                    ModelState.AddModelError(f.PropertyName, f.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
        }
    }
}
