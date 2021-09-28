using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepositApi.Models
{
    public class GetDepositsModel
    {
        public int StartIndex { get; set; }
        
        public int Count { get; set; }
    }
}
