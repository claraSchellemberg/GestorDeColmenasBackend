using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class CuadroException : LogicaDeNegocioException
    {
        public CuadroException(string message) : base(message) { }
    }
}
