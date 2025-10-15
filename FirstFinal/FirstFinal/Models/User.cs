using FirstFinal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public List<Account> Accounts { get; set; } = new();
        public List<Card> Cards { get; set; } = new();
    }
}
