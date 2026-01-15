using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Excepciones
{
    public class NotificacionException : LogicaDeNegocioException
    {
        public NotificacionException() : base() { }
        public NotificacionException(string message) : base(message) { }
        public NotificacionException(string message, Exception innerException) : base(message, innerException) { }
    }
    
    
}
