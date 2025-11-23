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
    public class RepositorioApiario : IRepositorioApiario
    {
        private GestorContext _context;
        public RepositorioApiario(GestorContext context)
        {
            _context = context;
        }
        public void Actualizar(Apiario entidad)
        {
            if(entidad!=null)
            {
                _context.Apiarios.Update(entidad);
                _context.SaveChanges();
            }
            else
            {
                throw new ApiarioException("El apiario no puede estar vacío");
            }
        }

        public void Agregar(Apiario entidad)
        {
            if (entidad != null)
            {
                _context.Apiarios.Add(entidad);
                _context.SaveChangesAsync();
            }
            else
            {
                throw new ApiarioException("El apiario no puede estar vacío");
            }
        }

        public void Eliminar(int id)
        {
            Apiario apiario = ObtenerElementoPorId(id);
            _context.Apiarios.Remove(apiario);
            _context.SaveChanges();
        }

        public Apiario ObtenerElementoPorId(int id)
        {
            Apiario apiario = _context.Apiarios.FirstOrDefault(a => a.Id == id);
            if(apiario !=null)
            {
                return apiario;
            }
            else
            {
                throw new ApiarioException("El apiario no existe");
            }
        }

        public IEnumerable<Apiario> ObtenerTodosLosElementos()
        {
            IEnumerable<Apiario> apiarios = _context.Apiarios
                .Include(apiario => apiario.Colmenas)
                .ThenInclude(colmena => colmena.Registros)
                .ToList();
            return apiarios;
        }
    }
}
