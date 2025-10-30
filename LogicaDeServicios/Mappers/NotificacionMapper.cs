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
        public static Notificacion FromDto(NotificacionDto notificacionDto)
        {
            return new Notificacion(notificacionDto.Mensaje,
                                    notificacionDto.RegistroAsociado);
        }

        public static NotificacionListadoDto ToDto(Notificacion notificacion)
        {
            return new NotificacionListadoDto(notificacion.Id,
                                             notificacion.Mensaje,
                                             notificacion.FechaNotificacion,
                                             notificacion.RegistroAsociado);
        }

        public static IEnumerable<NotificacionListadoDto> ToListDto(IEnumerable<Notificacion> notificacions)
        {
            List<NotificacionListadoDto> notificacionesListadoDto = new List<NotificacionListadoDto>();
            foreach(Notificacion notificacion in notificacions)
            {
                notificacionesListadoDto.Add(ToDto(notificacion));
            }
            return notificacionesListadoDto;

        }
    }
}
