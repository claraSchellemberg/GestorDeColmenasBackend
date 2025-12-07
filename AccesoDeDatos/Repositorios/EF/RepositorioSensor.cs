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
    public class RepositorioSensor : IRepositorioSensor
    {
        private GestorContext _context;
        public RepositorioSensor(GestorContext context)
        {
            _context = context;
        }
        public void Actualizar(Sensor entidad)
        {
            if (entidad != null)
            {
                _context.Sensores.Update(entidad);
                _context.SaveChanges();
            }
            else
            {
                throw new SensorException("El sensor no puede estar vacío");
            }
        }

        public void Agregar(Sensor entidad)
        {
            if (entidad != null)
            {
                _context.Sensores.Add(entidad);
                _context.SaveChangesAsync();
            }
            else
            {
                throw new SensorException("El sensor no puede estar vacío");
            }
        }

        public void Eliminar(int id)
        {
            Sensor sensor = ObtenerElementoPorId(id);
            _context.Sensores.Remove(sensor);
            _context.SaveChanges();
        }

        public Sensor ObtenerElementoPorId(int id)
        {
            Sensor sensor = _context.Sensores.FirstOrDefault(s => s.SensorId == id);
            if (sensor != null)
            {
                return sensor;
            }
            else
            {
                throw new SensorException("El sensor ingresado no existe");
            }
        }

        public IEnumerable<Sensor> ObtenerTodosLosElementos()
        {
            IEnumerable<Sensor> sensores = _context.Sensores
                .Include(sensor => sensor)
                .ToList();
            return sensores;
        }
    }
}
