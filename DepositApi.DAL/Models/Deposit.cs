using System;
using System.Collections.Generic;
using System.Text;

namespace DepositApi.DAL.Models
{
    public class Deposit
    {
        public int Id { get; set; }

        public double Amount { get; set; }

        public int Term { get; set; }

        public DateTime Date { get; set; }

        public double Percent { get; set; }
    }
}
