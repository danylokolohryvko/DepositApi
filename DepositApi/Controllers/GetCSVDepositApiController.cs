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
    [Route("/api/depositcalculation/csv")]
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
            var depositCalculetionCSV = await this.depositService.GetDepositCalculetionCSVAsync(depositId);

            return File(depositCalculetionCSV, "text/plain", "DepositCalculations.csv");
        }
    }
}
