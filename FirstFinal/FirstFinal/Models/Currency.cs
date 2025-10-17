using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Models
{
    public class Currency
    {
        public string Code { get; set; } = "GEL";
        public decimal RateToGEL { get; set; }
        public decimal RateToUSD { get; set; }
        public decimal RateToEUR { get; set; }
    }
}

