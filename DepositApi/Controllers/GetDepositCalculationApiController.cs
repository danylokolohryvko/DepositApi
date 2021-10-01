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
    [Route("/api/depositcalculation")]
    public class GetDepositCalculetionApiController : ControllerBase
    {
        private readonly IDepositService depositService;
        private readonly IMapper mapper;

        public GetDepositCalculetionApiController(IDepositService depositService, IMapper mapper)
        {
            this.depositService = depositService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get(GetDepositCalculetionsModel model)
        {
            var depositCalculetionDTOs = await this.depositService.GetDepositCalculetionsAsync(model.DepositId);
            var depositCalculetions = this.mapper.Map<List<DepositCalculationModel>>(depositCalculetionDTOs);

            return new JsonResult(depositCalculetions);
        }
    }
}
