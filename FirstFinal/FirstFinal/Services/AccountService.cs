using FirstFinal.Exceptions;
using FirstFinal.Interfaces;
using FirstFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;


        public AccountService(IUserService userService, ITransactionService transactionService)
        {
            _userService = userService;
            _transactionService = transactionService;
        }


        public decimal GetBalance(Guid accountId)
        {
            var account = FindAccountById(accountId);
            return account.Balance;
        }


        public void Deposit(Guid accountId, decimal amount)
        {
            var account = FindAccountById(accountId);
            account.Balance += amount;
            SaveChanges(account);


            _transactionService.LogTransaction(new Transaction
            {
                AccountId = account.Id,
                Type = Enums.TransactionType.Deposit,
                Amount = amount,
                Currency = account.Currency,
                Description = $"Deposited {amount} {account.Currency}"
            });
        }


        public void Withdraw(Guid accountId, decimal amount)
        {
            var account = FindAccountById(accountId);


            if (account.Balance < amount)
                throw new InsufficientFundsException("Insufficient funds.");


            account.Balance -= amount;
            SaveChanges(account);


            _transactionService.LogTransaction(new Transaction
            {
                AccountId = account.Id,
                Type = Enums.TransactionType.Withdrawal,
                Amount = amount,
                Currency = account.Currency,
                Description = $"Withdrew {amount} {account.Currency}"
            });
        }


        public Account GetAccountByCardNumber(string cardNumber)
        {
            var user = _userService.GetUserByCardNumber(cardNumber);
            return user?.Accounts.FirstOrDefault(a => a.CardNumber == cardNumber);
        }


        public List<Account> GetAllAccounts()
        {
            var users = _userService.LoadUsers();
            return users.SelectMany(u => u.Accounts).ToList();
        }


        private Account FindAccountById(Guid accountId)
        {
            var allAccounts = GetAllAccounts();
            var account = allAccounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
                throw new InvalidOperationException("Account not found.");
            return account;
        }


        private void SaveChanges(Account updatedAccount)
        {
            var users = _userService.LoadUsers();
            foreach (var user in users)
            {
                var account = user.Accounts.FirstOrDefault(a => a.Id == updatedAccount.Id);
                if (account != null)
                {
                    account.Balance = updatedAccount.Balance;
                    break;
                }
            }
            _userService.SaveUsers(users);
        }
    }

}
