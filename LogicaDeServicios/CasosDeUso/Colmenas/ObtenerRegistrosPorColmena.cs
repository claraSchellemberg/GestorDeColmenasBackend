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
        private readonly IRepositorioRegistroMedicionColmena _repoMedicionColmena;
        private readonly IRepositorioRegistroSensor _repoRegistroSensor;
        public ObtenerRegistrosPorColmena(IRepositorioColmena repoColmenas,
                                         IRepositorioRegistroMedicionColmena repoMedicionColmena,
                                            IRepositorioRegistroSensor repoRegistroSensor)
        {
            _repoColmenas = repoColmenas;
            _repoMedicionColmena = repoMedicionColmena;
            _repoRegistroSensor = repoRegistroSensor;
        }
        IEnumerable<RegistroPorColmenaDto> IObtenerRegistrosPorColmena<RegistroPorColmenaDto>.ObtenerRegistrosPorIdColmena(int idColmena)
        {
            var colmena = _repoColmenas.ObtenerElementoPorId(idColmena);
            if (colmena == null)
            {
                throw new ColmenaException($"La colmena con Id {idColmena} no existe");
            }
            var dtoList = new List<RegistroPorColmenaDto>();
            var registrosMedicion = _repoMedicionColmena.ObtenerRegistrosPorIdColmenaRepo(idColmena)
                ?? Enumerable.Empty<RegistroMedicionColmena>();
            foreach (var registro in registrosMedicion)
            {
                var dto = new RegistroPorColmenaDto
                {
                    Id = registro.Id,
                    FechaRegistro = registro.FechaRegistro,
                    EstaPendiente = registro.EstaPendiente,
                    ValorEstaEnRangoBorde = registro.ValorEstaEnRangoBorde,
                    MensajesAlerta = registro.MensajesAlerta,
                    TipoRegistro = "MedicionColmena",
                    FechaMedicion = registro.MedicionColmena.FechaMedicion,
                    TempInterna1 = 0,
                    TempInterna2 = 0,
                    TempInterna3 = 0,
                    TempExterna = registro.MedicionColmena.TempExterna,
                    Peso = registro.MedicionColmena.Peso,
                    Estado = registro.MedicionColmena.Colmena.Condicion
                };
                dtoList.Add(dto);
            }
            var registrosSensor = _repoRegistroSensor.ObtenerRegistrosPorIdColmenaRepo(idColmena)
            ?? Enumerable.Empty<RegistroSensor>();
            foreach (var registro in registrosSensor)
            {
                var dto = new RegistroPorColmenaDto
                {
                    Id = registro.Id,
                    FechaRegistro = registro.FechaRegistro,
                    EstaPendiente = registro.EstaPendiente,
                    ValorEstaEnRangoBorde = registro.ValorEstaEnRangoBorde,
                    MensajesAlerta = registro.MensajesAlerta,
                    TipoRegistro = "Sensor",
                    FechaMedicion = registro.SensorPorCuadro.FechaMedicion,
                    TempInterna1 = registro.SensorPorCuadro.TempInterna1,
                    TempInterna2 = registro.SensorPorCuadro.TempInterna2,
                    TempInterna3 = registro.SensorPorCuadro.TempInterna3,
                    TempExterna = 0,
                    Peso = 0
                };
                dtoList.Add(dto);
            }
            return dtoList.OrderBy(r => r.FechaRegistro);
        }
    }
}
