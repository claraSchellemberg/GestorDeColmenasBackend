using LogicaDeNegocios.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Usuarios
{
    public record UsuarioSetDto(string Nombre,
                                string Email,
                                string Contraseña,
                                string NumeroTelefono,
                                string NumeroApicultor,
                                CanalPreferidoNotificacion MedioDeComunicacionDePreferencia)
    {
    }
}
