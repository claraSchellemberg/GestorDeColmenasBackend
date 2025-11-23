using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Repositorios.Excepciones
{
    public class BadRequestException: AccesoDeDatosException
    {
        public BadRequestException() { }

        public BadRequestException(string? message) : base(message) { }

        protected BadRequestException(SerializationInfo info, StreamingContext context): base(info, context) { }

        public override int StatusCode()
        {
            return 400;
        }
    }
}
