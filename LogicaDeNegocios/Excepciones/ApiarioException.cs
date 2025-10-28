using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class ApiarioException : Exception
    {
        public ApiarioException() : base() { }
        public ApiarioException(string message) : base(message) { }
        public ApiarioException(string message, Exception innerException) : base(message, innerException) { }
    }
}
