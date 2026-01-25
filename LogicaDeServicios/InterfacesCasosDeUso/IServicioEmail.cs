using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.InterfacesCasosDeUso
{
    public interface IServicioEmail
    {
        Task EnviarAsync(Notificacion notificacion, Usuario usuario);
    }
}
