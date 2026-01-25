using LogicaDeNegocios.Entidades;
using LogicaDeServicios.InterfacesCasosDeUso;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Notificaciones.Canales
{
    public class CanalEmail : ICanalNotificacion
    {
        private readonly IServicioEmail _servicioEmail;
        public CanalEmail(IServicioEmail servicioEmail)
        {
            _servicioEmail = servicioEmail;
        }
        public async Task EnviarAsync(Notificacion notificacion, Usuario usuario)
        {
            await _servicioEmail.EnviarAsync(
            notificacion,
            usuario
            );
        }
    }
}