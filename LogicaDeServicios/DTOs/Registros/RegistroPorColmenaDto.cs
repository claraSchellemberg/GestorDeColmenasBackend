using LogicaDeNegocios.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Registros
{
    public class RegistroPorColmenaDto
    {
        // Metadatos del registro (ya existentes)
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EstaPendiente { get; set; }
        public bool ValorEstaEnRangoBorde { get; set; }
        public List<string>? MensajesAlerta { get; set; } = new();
        public string? TipoRegistro { get; set; }

        // Propiedades que la vista esperaba — pueden quedar con valores por defecto
        public DateTime FechaMedicion { get; set; }
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }
        public float TempExterna { get; set; }
        public float Peso { get; set; }

        // Estado / texto para mostrar (Estado puede venir del backend, EstadoTexto usa extensión)
        public CondicionColmena? Estado { get; set; }
        public string? MensajeEstado { get; set; }

    }
}
