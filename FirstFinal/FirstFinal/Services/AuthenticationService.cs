using FirstFinal.Exceptions;
using FirstFinal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;


        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }


        public bool ValidateCard(string cardNumber, DateTime validTill)
        {
            var user = _userService.GetUserByCardNumber(cardNumber);
            var card = user?.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);


            if (card == null)
                throw new InvalidCardException("Card not found.");


            if (card.ValidTill < DateTime.Now)
                throw new InvalidCardException("Card has expired.");


            if (card.ValidTill != validTill)
                throw new InvalidCardException("Invalid card validity date.");


            return true;
        }


        public bool ValidatePin(string cardNumber, string pin)
        {
            var user = _userService.GetUserByCardNumber(cardNumber);
            var card = user?.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);


            if (card == null)
                throw new InvalidCardException("Card not found.");


            if (card.Pin != pin)
                throw new InvalidPinException("Incorrect PIN.");


            if (card.IsBlocked)
                throw new InvalidCardException("Card is blocked.");


            return true;
        }


        public void ChangePin(string cardNumber, string oldPin, string newPin)
        {
            var user = _userService.GetUserByCardNumber(cardNumber);
            var card = user?.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);


            if (card == null)
                throw new InvalidCardException("Card not found.");


            if (card.Pin != oldPin)
                throw new InvalidPinException("Old PIN is incorrect.");


            card.Pin = newPin;
            var users = _userService.LoadUsers();
            _userService.SaveUsers(users);
        }
    }
}
