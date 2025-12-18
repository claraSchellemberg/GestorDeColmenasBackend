using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Repositorios.Excepciones
{
    public class NotFoundException : AccesoDeDatosException
    {
        public NotFoundException() { }

        public NotFoundException(string? message) : base(message) { }

        public override int StatusCode()
        {
            return 404;
        }
    }
}
