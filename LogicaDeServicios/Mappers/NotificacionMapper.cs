using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class NotificacionMapper
    {
        public static NotificacionGetDto ToDto(Notificacion notificacion)
        {
            return new NotificacionGetDto(notificacion.Id,
                                             notificacion.Mensaje,
                                             notificacion.FechaNotificacion,
                                             notificacion.Estado.ToString(),
                                             notificacion.RegistroAsociado.Id);
        }

        public static IEnumerable<NotificacionGetDto> ToListDto(IEnumerable<Notificacion> notificacions)
        {
            List<NotificacionGetDto> notificacionesGetDto = new List<NotificacionGetDto>();
            foreach(Notificacion notificacion in notificacions)
            {
                notificacionesGetDto.Add(ToDto(notificacion));
            }
            return notificacionesGetDto;

        }
    }
}
