using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class MedicionColmena
    {
        public int Id { get; set; }
        public float TempExterna { get; set; }
        public float Peso { get; set; }
        public DateTime FechaMedicion { get; set; }
        public int ColmenaId { get; set; }
        public MedicionColmena()
        {
        }
        public MedicionColmena(float tempExterna, float peso)
        {
            TempExterna = tempExterna;
            Peso = peso;
            this.FechaMedicion = DateTime.Now;
        }
    }
}
