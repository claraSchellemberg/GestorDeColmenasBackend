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
                entidad.ValidarApiario();
                _context.Entry(entidad).Property(a => a.FechaAlta).IsModified = false; //agrego esto para que entity no nos modifique la fecha de alta
                _context.SaveChanges();
            }
            else
            {
                throw new ApiarioException("El apiario no puede estar vacío");
            }
        }
        //antes del agregar hay que utilizar el find para verificar que ese nombre de
        //apiario ya no exista dentro de ese usuario
        //tambien implementarlo en colmena para los cuadros y tambiien se agrega la
        //constrain a nivel del EF
        public Apiario Agregar(Apiario entidad)
        {
            if (entidad != null)
            {
                //agrego la validacion del apiario
                entidad.ValidarApiario();
                _context.Apiarios.Add(entidad);
                _context.SaveChanges();
                return entidad;
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
                .ToList();
            return apiarios;
        }
    }
}
