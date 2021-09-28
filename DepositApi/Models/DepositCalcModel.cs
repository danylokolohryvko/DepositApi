using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepositApi.Models
{
    public class DepositCalcModel
    {
        public int Id { get; set; }

        public int Month { get; set; }

        public double PercentAdded { get; set; }

        public double TotalAmount { get; set; }

        public int DepositId { get; set; }
    }
}
