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

        public RegistroMedicionColmena() { }

        public RegistroMedicionColmena(MedicionColmena medicionColmena)
        {
            this.MedicionColmena = medicionColmena;
            ValorEstaEnRangoBorde = false;
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
            if (MedicionColmena.Peso > 0 && MedicionColmena.Peso <= pesoMinimo)
            {
                ValorEstaEnRangoBorde = true;
                MensajesAlerta.Add("Alerta: Peso de la colmena por debajo del umbral mínimo.");
            }

            float pesoMaximo = float.Parse(
                Configuracion.GetValorPorNombre("PesoMaximo"));
            if (MedicionColmena.Peso >= pesoMaximo)
            {
                ValorEstaEnRangoBorde = true;
                MensajesAlerta.Add("Alerta: Peso de la colmena por encima del umbral máximo. Vaya a cosechar.");
            }
        }
    }
}
