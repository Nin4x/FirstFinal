using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Interfaces
{
    public interface IAuthenticationService
    {
        bool ValidateCard(string cardNumber, DateTime validTill);
        bool ValidatePin(string cardNumber, string pin);
        void ChangePin(string cardNumber, string oldPin, string newPin);
    }
}
