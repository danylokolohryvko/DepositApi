using AutoMapper;
using DepositApi.BLL.Intrerfaces;
using DepositApi.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepositApi.Controllers
{
    [ApiController]
    [Route("/api/depositcalc/csv")]
    public class GetCSVDepositApiController : ControllerBase
    {
        private readonly IDepositService depositService;

        public GetCSVDepositApiController(IDepositService depositService, IMapper mapper)
        {
            this.depositService = depositService;
        }

        [HttpGet]
        public async Task<FileResult> Get(int depositId)
        {
            var depositCalcCSV = await this.depositService.GetDepositCalcCSVAsync(depositId);
            return File(depositCalcCSV, "text/plain", "DepositCalculations.csv");
        }
    }
}
