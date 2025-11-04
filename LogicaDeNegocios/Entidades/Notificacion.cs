using LogicaDeNegocios.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class Notificacion
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public Registro RegistroAsociado { get; set; }
        public Notificacion() { }
        public Notificacion( string mensaje, Registro registroAsociado)
        {
            this.Mensaje = mensaje;
            this.FechaNotificacion = DateTime.Now;
            RegistroAsociado = registroAsociado;
        }
        public void ValidarNotificacion()
        {
            if (string.IsNullOrEmpty(Mensaje) || RegistroAsociado == null)
            {
                throw new NotificacionException("El mensaje y el registro asociado son obligatorios.");
            }
        }
    }
}
