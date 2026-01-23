using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Registros
{
    public class RegistroMedicionColmenaGetDto : RegistroGetDto
    {
        public int MedicionColmenaId { get; set; }
        public float TempExterna { get; set; }
        public float Peso { get; set; }
        public DateTime FechaMedicion { get; set; }
        public int ColmenaId { get; set; }
        public RegistroMedicionColmenaGetDto() { }
        public RegistroMedicionColmenaGetDto(RegistroMedicionColmena registro)
        : base(registro)
        {
            if (registro?.MedicionColmena != null)
            {
                MedicionColmenaId = registro.MedicionColmena.Id;
                TempExterna = registro.MedicionColmena.TempExterna;
                Peso = registro.MedicionColmena.Peso;
                FechaMedicion = registro.MedicionColmena.FechaMedicion;
                ColmenaId = registro.MedicionColmena.ColmenaId;
            }
        }
    }

}
