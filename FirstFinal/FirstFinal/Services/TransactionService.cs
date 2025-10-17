using FirstFinal.Helpers;
using FirstFinal.Interfaces;
using FirstFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Services
{
    public class TransactionService : ITransactionService
    {
        private const string TransactionsFilePath = "C:\\Users\\n.kipshidze\\Desktop\\FirstFinal\\FirstFinal\\FirstFinal\\Data\\transactions.json";


        public void LogTransaction(Transaction transaction)
        {
            var transactions = GetAllTransactions();
            transactions.Add(transaction);
            JsonFileHelper.SaveToFile(TransactionsFilePath, transactions);
        }


        public List<Transaction> GetLastTransactions(Guid accountId, int count = 5)
        {
            return GetAllTransactions()
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.Date)
            .Take(count)
            .ToList();
        }


        public List<Transaction> GetAllTransactions()
        {
            return JsonFileHelper.LoadFromFile<Transaction>(TransactionsFilePath);
        }
    }
}
