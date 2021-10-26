using DepositApi.Core.Enums;
using System;

namespace DepositApi.Core.Models
{
    public class DepositModel
    {
        public int Id { get; set; }

        public decimal? Amount { get; set; }

        public int? Term { get; set; }
        
        public decimal? Percent { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public CalculationType? CalculationType { get; set; }
    }
}
