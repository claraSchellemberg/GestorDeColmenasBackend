using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Notificaciones
{
    public record NotificacionGetDto(int Id,
                                    string Mensaje,
                                    DateTime FechaNotificacion,
                                    string Estado,
                                    int? RegistroAsociadoId
    );
}
