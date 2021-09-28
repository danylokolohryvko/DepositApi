using System;
using System.Collections.Generic;
using System.Text;

namespace DepositApi.BLL.DTO
{
    public class DepositCalcDTO
    {
        public int Month { get; set; }

        public double PercentAdded { get; set; }

        public double TotalAmount { get; set; }
    }
}
