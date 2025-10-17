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
    public class UserService : IUserService
    {
        private const string UsersFilePath = "C:\\Users\\n.kipshidze\\Desktop\\FirstFinal\\FirstFinal\\FirstFinal\\Data\\users.json";


        public List<User> LoadUsers()
        {
            return JsonFileHelper.LoadFromFile<User>(UsersFilePath);
        }


        public void SaveUsers(List<User> users)
        {
            JsonFileHelper.SaveToFile(UsersFilePath, users);
        }


        public User GetUserByCardNumber(string cardNumber)
        {
            var users = LoadUsers();
            var user = users.First(u => u.Cards.Any(c => c.CardNumber == cardNumber));
            if (user == null)
                throw new Exceptions.InvalidOperationException("User not found for the given card number.");
            else
                return user;
        }

       
    }
}

