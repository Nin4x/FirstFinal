using FirstFinal.Services;
using FirstFinal.Interfaces;
using FirstFinal.Models;
using FirstFinal.Exceptions;
using FirstFinal.Helpers;
using System.Xml;

namespace FirstFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserService();
            ITransactionService transactionService = new TransactionService();
            IAccountService accountService = new AccountService(userService, transactionService);
            ICurrencyService currencyService = new CurrencyService();
            IAuthenticationService authService = new AuthenticationService(userService);
            ILoggerService logger = new LoggerService();


            bool exit = false;


            while (!exit)
            {
                MenuHelper.ShowMainMenu();
                var mainChoice = Console.ReadLine();


                switch (mainChoice)
                {
                    case "1":
                        Console.Write("Enter Card Number: ");
                        string cardNumber = Console.ReadLine();


                        Console.Write("Enter Card Valid Till (MM/yyyy): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime validTill))
                        {
                            Console.WriteLine($"Debugging: converted DateTime looks like this : {validTill}");
                            Console.WriteLine("Invalid date format.");
                            MenuHelper.Pause();
                            break;
                        }


                        try
                        {
                            if (authService.ValidateCard(cardNumber, validTill))
                            {
                                Console.Write("Enter PIN: ");
                                string pin_st = Console.ReadLine().Trim();
                                bool isNumeric = int.TryParse(pin_st, out int pin);
                                if (!isNumeric)
                                {
                                    throw new InvalidPinException("PIN must be numeric.");
                                }


                                if (authService.ValidatePin(cardNumber, pin.ToString()))
                                {
                                    HandleUserMenu(cardNumber, accountService, transactionService, currencyService, authService);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            logger.LogError(ex.Message);
                            MenuHelper.Pause();
                        }
                        break;


                    case "0":
                        exit = true;
                        break;


                    default:
                        Console.WriteLine("Invalid option.");
                        MenuHelper.Pause();
                        break;
                }
            }
        }


        static void HandleUserMenu(string cardNumber, IAccountService accountService, ITransactionService transactionService, ICurrencyService currencyService, IAuthenticationService authService)
        {
            bool back = false;


            while (!back)
            {
                MenuHelper.ShowUserMenu();
                var choice = Console.ReadLine();


                var account = accountService.GetAccountByCardNumber(cardNumber);
                if (account == null)
                {
                    Console.WriteLine("Account not found.");
                    return;
                }


                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine($"Current Balance: {account.Balance} {account.Currency}");
                            break;


                        case "2":
                            Console.Write("Enter amount to withdraw: ");
                            decimal withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                            accountService.Withdraw(account.Id, withdrawAmount);
                            Console.WriteLine("Withdrawal successful.");
                            break;


                        case "3":
                            Console.Write("Enter amount to deposit: ");
                            decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
                            accountService.Deposit(account.Id, depositAmount);
                            Console.WriteLine("Deposit successful.");
                            break;


                        case "4":
                            var lastTransactions = transactionService.GetLastTransactions(account.Id);
                            foreach (var t in lastTransactions)
                                Console.WriteLine($"{t.Date}: {t.Type} - {t.Amount} {t.Currency}");
                            break;


                        case "5":
                            Console.Write("Enter source currency (GEL/USD/EUR): ");
                            string from = Console.ReadLine().ToUpper();
                            Console.Write("Enter target currency (GEL/USD/EUR): ");
                            string to = Console.ReadLine().ToUpper();
                            Console.Write("Enter amount: ");
                            decimal amount = Convert.ToDecimal(Console.ReadLine());
                            decimal result = currencyService.Convert(from, to, amount);
                            Console.WriteLine($"Converted amount: {result:F2} {to}");
                            break;


                        case "6":
                            Console.Write("Enter old PIN: ");
                            int.TryParse(Console.ReadLine().Trim(), out int oldPin);
                            if (oldPin <= 0)
                            {
                               throw new InvalidPinException("PIN must be numeric.");
                            }
                            Console.Write("Enter new PIN: ");
                            int.TryParse(Console.ReadLine().Trim(), out int newPin);
                            if (newPin <= 0)
                            {
                                throw new InvalidPinException("PIN must be numeric.");
                            }   
                            authService.ChangePin(cardNumber, oldPin.ToString(), newPin.ToString());
                            Console.WriteLine("PIN changed successfully.");
                            break;


                        case "0":
                            back = true;
                            break;


                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }


                MenuHelper.Pause();
            }
        }
    }
}


            