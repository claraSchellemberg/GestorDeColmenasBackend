using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Notificaciones.Canales
{
    public class CanalWhatsApp : ICanalNotificacion
    { //esta logica no puede ser implementada al momento ya que no contamos con los recursos necesarios en el equipo
      //para llevarla adelante (no contamos con los fondos monetarios ni una cuenta aprobada de whatsApp Business)
      //pero se deja la clase para futura implementación si el cliente tuviera disposición para ello
        public Task EnviarAsync(Notificacion notificacion, Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
