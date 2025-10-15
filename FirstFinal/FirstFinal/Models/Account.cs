using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Models
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Currency { get; set; } = "GEL";
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public void UpdateBalance(decimal amount)
        {
            Balance += amount;
        }
    }

}
