using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Registros
{
    public class RegistroGetDto
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EstaPendiente { get; set; }
        public bool ValorEstaEnRangoBorde { get; set; }
        public List<string> MensajesAlerta { get; set; } = new();
        public string TipoRegistro { get; set; }

        public RegistroGetDto() { }
        public RegistroGetDto(Registro registro)
        {
            if (registro == null) return;
            Id = registro.Id;
            FechaRegistro = registro.FechaRegistro;
            EstaPendiente = registro.EstaPendiente;

            switch (registro)
            {
                case RegistroSensor rs:
                    ValorEstaEnRangoBorde = rs.ValorEstaEnRangoBorde;
                    MensajesAlerta = rs.MensajesAlerta ?? new List<string>();
                    TipoRegistro = "Sensor";
                    break;
                case RegistroMedicionColmena rm:
                    ValorEstaEnRangoBorde = rm.ValorEstaEnRangoBorde;
                    MensajesAlerta = rm.MensajesAlerta ?? new List<string>();
                    TipoRegistro = "MedicionColmena";
                    break;
                default:
                    TipoRegistro = "Registro";
                    break;
            }
        }
    }
}
