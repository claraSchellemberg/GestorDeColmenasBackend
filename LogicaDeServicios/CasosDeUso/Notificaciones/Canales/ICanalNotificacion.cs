using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Notificaciones.Canales
{
    public interface ICanalNotificacion
    {
        Task EnviarAsync(Notificacion notificacion, Usuario usuario);
    }
}
