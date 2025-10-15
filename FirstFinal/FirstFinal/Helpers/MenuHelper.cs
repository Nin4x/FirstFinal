using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Helpers
{
    public static class MenuHelper
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("===== ATM MAIN MENU =====");
            Console.WriteLine("1. Insert Card");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
        }


        public static void ShowUserMenu()
        {
            Console.Clear();
            Console.WriteLine("===== ACCOUNT MENU =====");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Last Five Transactions");
            Console.WriteLine("5. Currency Conversion");
            Console.WriteLine("6. Change PIN");
            Console.WriteLine("0. Return to Main Menu");
            Console.Write("Choose an option: ");
        }


        public static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
