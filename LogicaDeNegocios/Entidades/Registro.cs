using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class Registro
    {
        public int Id { get; set; }
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }

        public float TempExterna { get; set; }
        public float Peso { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Registro() { }
        public Registro( float tempInterna1, float tempInterna2, float tempInterna3,
                        float tempExterna, float peso)
        {
            this.TempInterna1 = tempInterna1;
            this.TempInterna2 = tempInterna2;
            this.TempInterna3 = tempInterna3;
            this.TempExterna = tempExterna;
            this.Peso = peso;
            this.FechaRegistro = DateTime.Now;
        }
        
    }
}
