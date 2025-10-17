using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Models
{
    public class Card
    {
        public string CardNumber { get; set; } = string.Empty;
        public DateTime ValidTill { get; set; }  
        public string Pin { get; set; } 
        public Guid AccountId { get; set; }
        public bool IsBlocked { get; set; } = false;

    }
}

  
