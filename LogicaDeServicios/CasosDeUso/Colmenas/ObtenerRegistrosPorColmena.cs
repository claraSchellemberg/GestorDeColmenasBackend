using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.Registros;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using LogicaDeServicios.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Colmenas
{
    public class ObtenerRegistrosPorColmena : IObtenerRegistrosPorColmena<RegistroPorColmenaDto>
    {
        private readonly IRepositorioColmena _repoColmenas;
        public ObtenerRegistrosPorColmena(IRepositorioColmena repoColmenas)
        {
            _repoColmenas = repoColmenas;
        }
        IEnumerable<RegistroPorColmenaDto> IObtenerRegistrosPorColmena<RegistroPorColmenaDto>.ObtenerRegistrosPorIdColmena(int idColmena)
        {
            var colmena = _repoColmenas.ObtenerElementoPorId(idColmena);
            if (colmena == null)
            {
                throw new ColmenaException($"La colmena con Id {idColmena} no existe");
            }
            var dtoList = new List<RegistroPorColmenaDto>();
            var registrosMedicion = colmena.Mediciones;
            foreach (var registro in registrosMedicion)
            {
                var dto = new RegistroPorColmenaDto
                {
                    Id = registro.Id,
                    TipoRegistro = "MedicionColmena",
                    FechaMedicion = registro.FechaMedicion,
                    TempInterna1 = 0,
                    TempInterna2 = 0,
                    TempInterna3 = 0,
                    TempExterna = registro.TempExterna,
                    Peso = registro.Peso,
                    Estado = registro.Colmena.Condicion
                };
                dtoList.Add(dto);
            }
            var cuadros = colmena.Cuadros;
            foreach(Cuadro c in cuadros)
            {
                foreach(SensorPorCuadro sr in c.Mediciones)
                {
                    var dto = new RegistroPorColmenaDto
                    {
                        Id = sr.Id,
                        TipoRegistro = "Sensor",
                        FechaMedicion = sr.FechaMedicion,
                        TempInterna1 = sr.TempInterna1,
                        TempInterna2 = sr.TempInterna2,
                        TempInterna3 = sr.TempInterna3
                    };
                    dtoList.Add(dto);
                }
            }
            return dtoList.OrderBy(r => r.FechaRegistro);
        }
    }
}
