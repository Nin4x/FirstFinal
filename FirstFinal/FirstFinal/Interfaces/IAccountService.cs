using FirstFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Interfaces
{
    public interface IAccountService
    {
        decimal GetBalance(Guid accountId);
        void Deposit(Guid accountId, decimal amount);
        void Withdraw(Guid accountId, decimal amount);
        Account GetAccountByCardNumber(string cardNumber);
        List<Account> GetAllAccounts();
    }
}
