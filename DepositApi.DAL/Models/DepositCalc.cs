using System;
using System.Collections.Generic;
using System.Text;

namespace DepositApi.DAL.Models
{
    public class DepositCalc
    {
        public int Id { get; set; }

        public int Month { get; set; }

        public double PercentAdded { get; set; }

        public double TotalAmount { get; set; }

        public int DepositId { get; set; }
    }
}
