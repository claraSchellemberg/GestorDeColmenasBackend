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
    public class RepositorioColmena : IRepositorioColmena
    {
        private GestorContext _context;
        public RepositorioColmena(GestorContext context)
        {
            _context = context;
        }
        public void Actualizar(Colmena entidad)
        {
            if (entidad != null)
            {
                _context.Colmenas.Update(entidad);
                entidad.ValidarColmena();
                _context.Entry(entidad).Property(a=> a.FechaInstalacionSensores).IsModified = false;//agrego esto para que entity no nos modifique la fecha de alta de los sensores
                _context.SaveChanges();
            }
            else
            {
                throw new ColmenaException("El apiario no puede estar vacío");
            }
        }
        public void Agregar(Colmena entidad)
        {
            if (entidad != null)
            {
                entidad.ValidarColmena();
                _context.Colmenas.Add(entidad);
                _context.SaveChangesAsync();
            }
            else
            {
                throw new ColmenaException("La colmena no puede estar vacío");
            }
        }
        public void Eliminar(int id)
        {
            Colmena colmena = ObtenerElementoPorId(id);
            _context.Colmenas.Remove(colmena);
            _context.SaveChanges();
        }
        public Colmena ObtenerElementoPorId(int id)
        {
            Colmena colmena = _context.Colmenas.FirstOrDefault(a => a.Id == id);
            if (colmena != null)
            {
                return colmena;
            }
            else
            {
                throw new ColmenaException("La colmena no existe");
            }
        }
       
        public IEnumerable<Colmena> ObtenerTodosLosElementos()
        {
            IEnumerable<Colmena> colmenas = _context.Colmenas
                .Include(c => c.Cuadros)
                .Include(c => c.Mediciones)
                .ToList();
            return colmenas;
        }

        public IEnumerable<Colmena> ObtenerColmenasPorApiario(int apiarioId)
        {
            IEnumerable<Colmena> colmenas = _context.Colmenas
                .Where(c => c.ApiarioId == apiarioId)
                .Include(c => c.Cuadros)
                .Include(c => c.Mediciones)
                .ToList();
            if(!colmenas.Any())
            {
                throw new ColmenaException("No se encontraron colmenas para el apiario especificado.");
            }
            return colmenas;
        }
    }
}
