using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Notificaciones.Canales
{
    internal class CanalFrontend : ICanalNotificacion
    {
        public Task EnviarAsync(Notificacion notificacion, Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
