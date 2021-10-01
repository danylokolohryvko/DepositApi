using System;
using System.Collections.Generic;
using System.Text;

namespace DepositApi.BLL.DTO
{
    public class DepositDTO
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }

        public DateTime Date { get; set; }
    }
}
