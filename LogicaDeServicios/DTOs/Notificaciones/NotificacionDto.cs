using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Notificaciones
{
    public record NotificacionDto(string Mensaje,
                                  Registro RegistroAsociado)
    {
    }
}
