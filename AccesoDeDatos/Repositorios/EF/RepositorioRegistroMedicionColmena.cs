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
    public class RepositorioRegistroMedicionColmena : IRepositorioRegistroMedicionColmena
    {
        private GestorContext _context;
        public RepositorioRegistroMedicionColmena(GestorContext context)
        {
            _context = context;
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
        public RegistroMedicionColmena ObtenerElementoPorId(int id)
        {
            RegistroMedicionColmena registroMedicionColmena = _context.RegistroMedicionColmenas
                .FirstOrDefault(rs => rs.Id == id);
            if (registroMedicionColmena != null)
            {
                return registroMedicionColmena;
            }
            else
            {
                throw new RegistroException("El registro ingresado no existe");
            }
        }

        public IEnumerable<RegistroMedicionColmena> ObtenerRegistrosPorIdColmenaRepo(int idColmena)
        {
            return _context.RegistroMedicionColmenas
                .Where(r => r.MedicionColmena.ColmenaId == idColmena)
                .Include(r => r.MedicionColmena)
                .ThenInclude(mc => mc.Colmena)
                .ToList();
        }

        public IEnumerable<RegistroMedicionColmena> ObtenerTodosLosElementos()
        {
            IEnumerable<RegistroMedicionColmena> registros = _context.RegistroMedicionColmenas
                .Include(rs => rs.MedicionColmena)
                .ToList();
            return registros;
        }
    }
}
