using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Interfaces
{
    public interface ICurrencyService
    {
        decimal Convert(string fromCurrency, string toCurrency, decimal amount);
    }
}
