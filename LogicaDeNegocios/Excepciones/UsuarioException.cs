using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class UsuarioException : Exception
    {
        public UsuarioException(string message) : base(message) { }
    }
}
