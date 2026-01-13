using LogicaDeNegocios.Entidades;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Notificaciones.Canales
{
    public class CanalSms : ICanalNotificacion
    {
        private readonly IServicioSms _servicioSms;
        public CanalSms(IServicioSms servicioSms)
        {
            _servicioSms = servicioSms;
        }
        public async Task EnviarAsync(Notificacion notificacion, Usuario usuario)
        {
            await _servicioSms.EnviarAsync(
            usuario.NumeroTelefono,
            notificacion.Mensaje
            );
        }
    }
}
