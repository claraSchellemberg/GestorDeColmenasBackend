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
    public class RepositorioRegistroSensor : IRepositorioRegistroSensor
    {
        private GestorContext _context;
        public RepositorioRegistroSensor(GestorContext context)
        {
            _context = context;
        }

        public RegistroSensor Agregar(RegistroSensor entidad)
        {
            if (entidad != null)
            {
                _context.RegistroSensors.Add(entidad);
                _context.SaveChanges();
                return entidad;
            }
            else
            {
                throw new SensorException("El sensor no puede estar vacío");
            }
        }
        public RegistroSensor ObtenerElementoPorId(int id)
        {
            RegistroSensor registroSensor = _context.RegistroSensors.FirstOrDefault(rs => rs.Id == id);
            if (registroSensor != null)
            {
                return registroSensor;
            }
            else
            {
                throw new SensorException("El sensor ingresado no existe");
            }
        }

        public IEnumerable<RegistroSensor> ObtenerRegistrosPorIdColmenaRepo(int idColmena)
        {
            return _context.RegistroSensors
                .Where(rs => rs.SensorPorCuadro.Cuadro.ColmenaId == idColmena)
                .Include(rs => rs.SensorPorCuadro)
                .ThenInclude(sc => sc.Cuadro)
                .ThenInclude(c => c.Colmena)
                .ToList();
        }

        public IEnumerable<RegistroSensor> ObtenerTodosLosElementos()
        {
            IEnumerable<RegistroSensor> registros = _context.RegistroSensors
                .Include(rs => rs.SensorPorCuadro)
                .ThenInclude(sc => sc.Cuadro)
                .ThenInclude(c => c.Colmena)
                .ToList();
            return registros;
        }

        public RegistroSensor ObtenerUltimoPorCuadro(int cuadroId)
        {
            var ultimoRegistro = ObtenerTodosLosElementos()
                                .Where(r => r.SensorPorCuadro.CuadroId == cuadroId)
                                .OrderByDescending(r => r.FechaRegistro)
                                .FirstOrDefault(); // FirstOrDefault retorna null si no hay elementos
            
            return ultimoRegistro;
        }
    }
}
