using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Interfaces
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}
