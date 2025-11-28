using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioSensor : IRepositorioAgregar<Sensor>,
                        IRepositorioActualizar<Sensor>, IRepositorioEliminar<Sensor>,
                        IRepositorioObtenerPorId<Sensor>, IRepositorioObtenerTodos<Sensor>
    {
    }
}
