using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.Registros;
using LogicaDeServicios.DTOs.Colmenas;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Colmenas
{
    public class ObtenerDetalleColmena : IObtenerDetalleColmena<DetalleColmenaDto>
    {
        private readonly IRepositorioColmena _repoColmenas;
        private readonly IRepositorioRegistro _repoRegistros;
        public ObtenerDetalleColmena(IRepositorioColmena repoColmenas,
                                        IRepositorioRegistro repoRegistros)
        {
            _repoColmenas = repoColmenas;
            _repoRegistros = repoRegistros;
        }

        DetalleColmenaDto IObtenerDetalleColmena<DetalleColmenaDto>.ObtenerDetalleColmena(int idColmena)
        {
            Colmena colmena = _repoColmenas.ObtenerElementoPorId(idColmena);
            if (colmena == null)
            {
                throw new ColmenaException($"La colmena con Id {idColmena} no existe");
            }
            int cantCuadros = colmena.Cuadros.Count;
            int cantRegistros = colmena.Mediciones.Count;
            float tempInterna1 = ObtenerUltimaMedicion(colmena, m => m.TempInterna1);
            float tempInterna2 = ObtenerUltimaMedicion(colmena, m => m.TempInterna2);
            float tempInterna3 = ObtenerUltimaMedicion(colmena, m => m.TempInterna3);
            float tempExterna = ObtenerUltimaMedicionColmena(colmena, m => m.TempExterna);
            float peso = ObtenerUltimaMedicionColmena(colmena, m => m.Peso);

            return new DetalleColmenaDto(idColmena, colmena.Nombre, colmena.Descripcion, 
                colmena.Apiario.Nombre,
                colmena.FechaInstalacionSensores, colmena.Condicion, cantCuadros, cantRegistros,
                tempInterna1, tempInterna2, tempInterna3, tempExterna, peso);
        }

        private float ObtenerUltimaMedicion(Colmena colmena, Func<SensorPorCuadro, float> selector)
        {
            return colmena.Cuadros
                .SelectMany(c => c.Mediciones)
                .OrderByDescending(m => m.FechaMedicion)
                .Select(selector)
                .FirstOrDefault();
        }
        private float ObtenerUltimaMedicionColmena(Colmena colmena, Func<MedicionColmena, float> selector)
        {
            return colmena.Mediciones
                .OrderByDescending(m => m.FechaMedicion)
                .Select(selector)
                .FirstOrDefault();
        }
    }
}
