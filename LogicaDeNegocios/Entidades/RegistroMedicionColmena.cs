using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class RegistroMedicionColmena : Registro
    {
        public MedicionColmena MedicionColmena { get; set; }
        public bool ValorEstaEnRangoBorde { get; set; }
        public List<string> MensajesAlerta { get; set; } = new List<string>();

        private readonly ILogger<RegistroMedicionColmena> _logger;

        public RegistroMedicionColmena() { }

        public RegistroMedicionColmena(MedicionColmena medicionColmena)
        {
            this.MedicionColmena = medicionColmena;
            ValorEstaEnRangoBorde = false;
        }

        //controlador que acepta logs para ver desde azure
        public RegistroMedicionColmena(MedicionColmena medicionColmena, ILogger<RegistroMedicionColmena> logger)
        {
            MedicionColmena = medicionColmena;
            ValorEstaEnRangoBorde = false;
            _logger = logger ?? NullLogger<RegistroMedicionColmena>.Instance;
        }

        public override void ControlarValores()
        {
            // Validamos que la colmena no se haya caido o que no haya un error en la medición
            if (MedicionColmena.Peso == 0)
            {
                ValorEstaEnRangoBorde = true;
                MensajesAlerta.Add("Alerta: Peso de la colmena es cero. Verifique que se encuentre todo en orden.");
            }

            float pesoMinimo = float.Parse(
                Configuracion.GetValorPorNombre("PesoMinimoColmena"));
            _logger.LogInformation($"Peso minimo configurado: {pesoMinimo}");
            if (MedicionColmena.Peso > 0 && MedicionColmena.Peso <= pesoMinimo)
            {
                ValorEstaEnRangoBorde = true;
                MensajesAlerta.Add("Alerta: Peso de la colmena por debajo del umbral mínimo.");
            }

            float pesoMaximo = float.Parse(
                Configuracion.GetValorPorNombre("PesoMaximo"));
            _logger.LogInformation($"PesoMaximo configurado: {pesoMaximo}");
            if (MedicionColmena.Peso >= pesoMaximo)
            {
                ValorEstaEnRangoBorde = true;
                MensajesAlerta.Add("Alerta: Peso de la colmena por encima del umbral máximo. Vaya a cosechar.");
            }
        }
    }
}
