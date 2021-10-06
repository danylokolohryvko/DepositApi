using AutoMapper;
using DepositApi.BLL.Intrerfaces;
using Microsoft.AspNetCore.Mvc;
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
            var depositCalculationCSV = await this.depositService.GetDepositCalculationCSVAsync(depositId);

            return File(depositCalculationCSV, "text/plain", "DepositCalculations.csv");
        }
    }
}
