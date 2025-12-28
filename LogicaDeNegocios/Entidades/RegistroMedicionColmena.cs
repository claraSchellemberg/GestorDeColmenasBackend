using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class RegistroMedicionColmena: Registro
    {
        public MedicionColmena MedicionColmena { get; set; }
        public RegistroMedicionColmena() { }
        public RegistroMedicionColmena(MedicionColmena medicionColmena)
        {
            this.MedicionColmena = medicionColmena;
        }

        public override void ControlarValores()
        {
            float pesoMinimo = float.Parse(
                Configuracion.Instancia.GetValorPorNombre("PesoMinimoColmena"));
            if (MedicionColmena.Peso <= pesoMinimo)
            {
                throw new Exception("Alerta: Peso de la colmena por debajo del umbral mínimo.");
            }

            float pesoMaximo = float.Parse(
                Configuracion.Instancia.GetValorPorNombre("PesoMaximo"));
            if (MedicionColmena.Peso >= pesoMaximo)
            {
                throw new Exception("Alerta: Peso de la colmena por encima del umbral máximo. Vaya a cosechar");
            }
        }
    }
}
