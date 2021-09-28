using System;
using System.Collections.Generic;
using System.Text;

namespace DepositApi.BLL.DTO
{
    public class DepositCalcDTO
    {
        public int Id { get; set; }

        public int Month { get; set; }

        public double PercentAdded { get; set; }

        public double TotalAmount { get; set; }

        public int DepositId { get; set; }
    }
}
