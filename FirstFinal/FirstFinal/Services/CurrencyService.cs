using FirstFinal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly Dictionary<string, decimal> _ratesToGEL = new()
        {
            { "GEL", 1m },
            { "USD", 2.7m },
            { "EUR", 3m }
        };


        public decimal Convert(string fromCurrency, string toCurrency, decimal amount)
        {
            if (!_ratesToGEL.ContainsKey(fromCurrency) || !_ratesToGEL.ContainsKey(toCurrency))
                throw new ArgumentException("Unsupported currency.");


            decimal amountInGEL = amount * _ratesToGEL[fromCurrency] / _ratesToGEL[fromCurrency];


            if (fromCurrency == "GEL")
                return amount / _ratesToGEL[toCurrency];


            if (toCurrency == "GEL")
                return amount * _ratesToGEL[fromCurrency];


            decimal inGEL = amount * _ratesToGEL[fromCurrency];
            return inGEL / _ratesToGEL[toCurrency];
        }
    }
}
