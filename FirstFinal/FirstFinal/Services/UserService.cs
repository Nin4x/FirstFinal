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
        private const string UsersFilePath = "Data/users.json";


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
            return users.FirstOrDefault(u => u.Cards.Any(c => c.CardNumber == cardNumber));
        }
    }
}
}
