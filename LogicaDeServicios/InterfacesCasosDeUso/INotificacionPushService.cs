using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.InterfacesCasosDeUso
{
    public interface INotificacionPushService
    {
        Task EnviarNotificacionAsync(int usuarioId, Notificacion notificacion);
    }
}
