using LogicaDeNegocios.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public abstract class Registro
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EstaPendiente { get; set; } = true;
        public Registro() 
        {
            this.FechaRegistro = DateTime.Now;
        }

        public abstract void ControlarValores();
    }
}
