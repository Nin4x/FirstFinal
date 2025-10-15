using FirstFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Interfaces
{
    public interface ITransactionService
    {
        void LogTransaction(Transaction transaction);
        List<Transaction> GetLastTransactions(Guid accountId, int count = 5);
        List<Transaction> GetAllTransactions();
    }
}
