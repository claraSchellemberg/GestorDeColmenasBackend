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
                .Include(c => c.Registros).ToList();
            return colmenas;
        }
    }
}
