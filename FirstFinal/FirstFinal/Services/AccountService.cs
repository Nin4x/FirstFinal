using FirstFinal.Exceptions;
using FirstFinal.Interfaces;
using FirstFinal.Models;
using FirstFinal.Services;
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

        public Account FindAccountById(Guid accountId)
        {
            var users = _userService.LoadUsers();
            foreach (var user in users)
            {

                Console.WriteLine($"Debugging: loaded user accountId-s  {user.Account.Id}");
                if (user.Account.Id == accountId)
                {
                    return user.Account;
                }
            }
            throw new Exceptions.InvalidOperationException("Account not found.");
        }

        public decimal GetBalance(Guid accountId)
        {
            var account = FindAccountById(accountId);
            return account.Balance;
        }


        public void Deposit(Guid accountId, decimal amount)
        {
            Console.WriteLine($"Debugging: accountID used in finding user: {accountId}");
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
            if (user != null) { 
                var account = user.Account;

                Console.WriteLine($"Debugging: ID of the Account Returned: {account.Id}");
                if (account != null)
                    return account;
                else
                    throw new Exceptions.InvalidOperationException("Account not found for the given card number.");
            }
            else
            {
                throw new Exceptions.InvalidOperationException("User not found for the given card number.");
            }
            
        }


        //public List<Account> GetAllAccounts()
        //{
        //    var users = _userService.LoadUsers();
        //    return users.SelectMany(u => u.Accounts).ToList();
        //}


        //private Account FindAccountById(Guid accountId)
        //{
        //    var allAccounts = GetAllAccounts();
        //    var account = allAccounts.FirstOrDefault(a => a.Id == accountId);
        //    if (account == null)
        //        throw new Exceptions.InvalidOperationException("Account not found.");
        //    return account;
        //}


        //public List<Account> GetAllAccounts()
        //{
        //    var users = _userService.LoadUsers();
        //    return users.SelectMany(u => u.Accounts).ToList();
        //}


        //private Account FindAccountById(Guid accountId)
        //{
        //    var allAccounts = GetAllAccounts();
        //    var account = allAccounts.FirstOrDefault(a => a.Id == accountId);
        //    if (account == null)
        //        throw new Exceptions.InvalidOperationException("Account not found.");
        //    return account;
        //}


        private void SaveChanges(Account updatedAccount)
        {
            var users = _userService.LoadUsers();
            foreach (var user in users)
            {
                var account = user.Account;
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
