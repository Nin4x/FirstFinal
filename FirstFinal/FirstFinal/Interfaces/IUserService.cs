using FirstFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Interfaces
{
    public interface IUserService
    {
        List<User> LoadUsers();
        void SaveUsers(List<User> users);
        User GetUserByCardNumber(string cardNumber);
    }
}
