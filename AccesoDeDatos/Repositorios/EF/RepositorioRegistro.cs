using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio.Registros;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Repositorios.EF
{
    public class RepositorioRegistro : IRepositorioRegistro
    {
        private GestorContext _context;
        public RepositorioRegistro(GestorContext context)
        {
            _context = context;
        }
        public void Actualizar(Registro entidad)
        {
            if (entidad != null)
            {
                _context.Registros.Update(entidad);
                _context.SaveChanges();
            }
            else
            {
                throw new RegistroException("El registro no puede estar vacío");
            }
        }

        public Registro Agregar(Registro entidad)
        {
            if (entidad != null)
            {
                _context.Registros.Add(entidad);
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
            Registro registro = ObtenerElementoPorId(id);
            _context.Registros.Remove(registro);
            _context.SaveChanges();
        }

        public Registro ObtenerElementoPorId(int id)
        {
            Registro registro = _context.Registros.FirstOrDefault(a => a.Id == id);
            if (registro != null)
            {
                return registro;
            }
            else
            {
                throw new RegistroException("El Registro no existe");
            }
        }

        public IEnumerable<Registro> ObtenerTodosLosElementos()
        {
            IEnumerable<Registro> registros = _context.Registros
                .ToList();
            return registros;
        }
    }
}
