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
    public class DepositCalculationApiController : ControllerBase
    {
        private readonly IDepositService depositService;
        private readonly IMapper mapper;

        public DepositCalculationApiController(IDepositService depositService, IMapper mapper)
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
                var depositCalculetionDTO = await this.depositService.PersentCalculationAsync(depositDTO);
                var depositCalculetion = this.mapper.Map<List<DepositCalculationModel>>(depositCalculetionDTO);

                return new JsonResult(depositCalculetion);
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
