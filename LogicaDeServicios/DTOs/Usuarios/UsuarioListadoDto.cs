using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Usuarios
{
    public record UsuarioListadoDto(int Id,
                                    string Nombre,
                                    string Email,
                                    string Contraseña)
    {
    }
}
