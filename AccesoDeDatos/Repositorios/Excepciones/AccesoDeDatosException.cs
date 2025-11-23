using LogicaDeNegocios.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Repositorios.Excepciones
{
    public abstract class AccesoDeDatosException: Exception
    {
        string _message;

        public AccesoDeDatosException()
        {}

        public AccesoDeDatosException(string? message): base(message)
        {
            _message = message;
        }

        protected AccesoDeDatosException(SerializationInfo info, StreamingContext context) : base(info, context)
        {}

       public abstract int StatusCode();

        public Error Error()
        {
            return new Error(
                StatusCode(),
                _message
                );
            
        }
    }
}
