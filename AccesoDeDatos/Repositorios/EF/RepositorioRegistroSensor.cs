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
    public class RepositorioRegistroSensor : IRepositorioRegistroSensor
    {
        private GestorContext _context;
        public RepositorioRegistroSensor(GestorContext context)
        {
            _context = context;
        }

        public void Agregar(RegistroSensor entidad)
        {
            throw new NotImplementedException();
        }
        public RegistroSensor ObtenerElementoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegistroSensor> ObtenerTodosLosElementos()
        {
            IEnumerable<RegistroSensor> registros = _context.RegistroSensors
                .Include(rs => rs.sensorPorCuadro)
                .ToList();
            return registros;
        }

        public RegistroSensor ObtenerUltimoPorCuadro(int cuadroId)
        {
            var ultimoRegistro = ObtenerTodosLosElementos()
                                .Where(r => r.sensorPorCuadro.CuadroId == cuadroId)
                                .OrderByDescending(r => r.FechaRegistro)
                                .FirstOrDefault(); // FirstOrDefault retorna null si no hay elementos
            
            return ultimoRegistro;
        }
    }
}
