using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class LogicaDeNegocioException: Exception
    {
        private string _message;

        public LogicaDeNegocioException()
        {
        }

        public LogicaDeNegocioException(string? message): base(message) 
        {
            _message = message;
        }

        public LogicaDeNegocioException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public Error Error()
        {
            return new Error(400,
                            _message);
        }
    }
}
