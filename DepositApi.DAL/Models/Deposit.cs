using System;
using System.Collections.Generic;
using System.Text;

namespace DepositApi.DAL.Models
{
    public class Deposit
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int Term { get; set; }

        public DateTime Date { get; set; }

        public decimal Percent { get; set; }
    }
}
