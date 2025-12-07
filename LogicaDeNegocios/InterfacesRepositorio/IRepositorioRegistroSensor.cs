using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioRegistroSensor: IRepositorioAgregar<RegistroSensor>,
                        IRepositorioActualizar<RegistroSensor>, IRepositorioEliminar<RegistroSensor>,
                        IRepositorioObtenerPorId<RegistroSensor>, IRepositorioObtenerTodos<RegistroSensor>
    {
    }
}
