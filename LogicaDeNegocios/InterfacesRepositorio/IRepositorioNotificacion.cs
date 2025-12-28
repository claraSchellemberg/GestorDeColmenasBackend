using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioNotificacion : IRepositorioAgregar<Notificacion>,
                                                IRepositorioObtenerPorId<Notificacion>,
                                                IRepositorioObtenerTodos<Notificacion>
    {
        /// <summary>
        /// Obtiene todas las notificaciones pendientes de un usuario
        /// </summary>
        IEnumerable<Notificacion> ObtenerNotificacionesPendientes(int usuarioId);
    }
}
