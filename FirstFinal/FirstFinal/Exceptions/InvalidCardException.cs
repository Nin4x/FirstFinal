using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Exceptions
{
    public class InvalidCardException : Exception
    {
        public InvalidCardException(string message) : base(message) { }
    }
}
