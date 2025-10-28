using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class RegistroException : Exception
    {
        public RegistroException() : base() { }
        public RegistroException(string message) : base(message) { }
        public RegistroException(string message, Exception innerException) : base(message, innerException) { }
    }
}
