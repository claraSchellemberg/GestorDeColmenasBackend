using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Registros
{
    public class RegistroSensorGetDto : RegistroGetDto
    {
        public int SensorPorCuadroId { get; set; }
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }
        public DateTime FechaMedicion { get; set; }
        public int SensorId { get; set; }
        public int CuadroId { get; set; }
        public string TipoSensor { get; set; }
        public RegistroSensorGetDto() { }

        public RegistroSensorGetDto(RegistroSensor registro)
            : base(registro)
        {
            var spc = registro?.SensorPorCuadro;
            if (spc != null)
            {
                SensorPorCuadroId = spc.Id;
                TempInterna1 = spc.TempInterna1;
                TempInterna2 = spc.TempInterna2;
                TempInterna3 = spc.TempInterna3;
                FechaMedicion = spc.FechaMedicion;
                SensorId = spc.SensorId;
                CuadroId = spc.CuadroId;
                TipoSensor = spc.Sensor?.TipoSensor;
            }
        }
    }
}