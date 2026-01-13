using LogicaDeNegocios.Enums;
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
        public Usuario UsuarioReceptor { get; set; }
        public EstadoNotificacion Estado { get; set; }

        //falta un estado (creado y esta pendiente, enviado y leido), esta las va a levantar el observer
        //alguien tiene que grabar una notificacion (en creaado y pendiente)
        //el observer va a agarrar los objetos creados y penidentes y le envia la notificacion al ususario,
        //le cambio el estado a enviado y genera por ej el wpp
        //el usuario leyo el wpp y lee la notificacion y la misma pasa a estado leido
        //en el front una campanita que avise que hay notificaciones pendientes de leer y al ingresar avisamos al
        //back que fue leida pro el usuario
        public Notificacion() { }
        public Notificacion( string mensaje, Registro registroAsociado, Usuario usuarioAsociado)
        {
            this.Mensaje = mensaje;
            this.FechaNotificacion = DateTime.Now;
            RegistroAsociado = registroAsociado;
            this.UsuarioReceptor = usuarioAsociado;
            this.Estado = EstadoNotificacion.PENDIENTE;
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
