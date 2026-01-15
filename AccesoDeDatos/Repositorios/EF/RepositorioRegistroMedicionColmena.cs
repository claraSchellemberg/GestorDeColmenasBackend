using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Repositorios.EF
{
    public class RepositorioRegistroMedicionColmena : IRepositorioRegistroMedicionColmena
    {
        private GestorContext _context;
        public RepositorioRegistroMedicionColmena(GestorContext context)
        {
            _context = context;
        }
        public void Actualizar(RegistroMedicionColmena entidad)
        {
            throw new NotImplementedException();
        }

        public RegistroMedicionColmena Agregar(RegistroMedicionColmena entidad)
        {
            if (entidad != null)
            {
                _context.RegistroMedicionColmenas.Add(entidad);
                _context.SaveChanges();
                return entidad;
            }
            else
            {
                throw new RegistroException("El registro no puede estar vacío");
            }
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public RegistroMedicionColmena ObtenerElementoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegistroMedicionColmena> ObtenerTodosLosElementos()
        {
            throw new NotImplementedException();
        }
    }
}
