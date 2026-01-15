using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class ColmenaException : LogicaDeNegocioException
    {
        public ColmenaException() : base() { }
        public ColmenaException(string message) : base(message) { }
        public ColmenaException(string message, Exception innerException) : base(message, innerException) { }
    }
}
