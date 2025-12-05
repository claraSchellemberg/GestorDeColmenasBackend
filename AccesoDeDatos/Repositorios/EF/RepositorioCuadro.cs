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
    public class RepositorioCuadro : IRepositorioCuadro
    {
        private GestorContext _context;
        public RepositorioCuadro(GestorContext context)
        {
            _context = context;
        }
        public void Actualizar(Cuadro entidad)
        {
            if (entidad != null)
            {
                _context.Cuadros.Update(entidad);
                _context.SaveChanges();
            }
            else
            {
                throw new CuadroException("El Cuadro no puede estar vacío");
            }
        }

        public void Agregar(Cuadro entidad)
        {
            if (entidad != null)
            {
                _context.Cuadros.Add(entidad);
                _context.SaveChangesAsync();
            }
            else
            {
                throw new CuadroException("El apiario no puede estar vacío");
            }
        }

        public void AgregarMedicionDeCuadro(SensorPorCuadro medicion, Cuadro cuadro)
        {
            cuadro.Mediciones.Add(medicion);
            Actualizar(cuadro);
        }

        public void Eliminar(int id)
        {
            Cuadro cuadro = ObtenerElementoPorId(id);
            _context.Cuadros.Remove(cuadro);
            _context.SaveChanges();
        }

        public Cuadro ObtenerElementoPorId(int id)
        {
            Cuadro cuadro = _context.Cuadros.FirstOrDefault(c => c.Id == id);
            if (cuadro != null)
            {
                return cuadro;
            }
            else
            {
                throw new CuadroException("El cuadro que busca no existe");
            }
        }

        public IEnumerable<Cuadro> ObtenerTodosLosElementos()
        {
            IEnumerable<Cuadro> cuadros = _context.Cuadros
                            .Include(cuadro => cuadro.Mediciones)
                            .ThenInclude(sensorPorCuadro => sensorPorCuadro.Sensor)
                            .ToList();
            return cuadros;
        }
    }
}
