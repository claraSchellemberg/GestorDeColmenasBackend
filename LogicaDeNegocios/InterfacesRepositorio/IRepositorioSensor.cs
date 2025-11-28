using LogicaDeNegocios.Entidades;
<<<<<<< HEAD
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
=======
>>>>>>> origin/Develop
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
<<<<<<< HEAD
    public interface IRepositorioSensor : IRepositorioAgregar<Sensor>,
=======
    public interface IRepositorioSensor: IRepositorioAgregar<Sensor>,
>>>>>>> origin/Develop
                        IRepositorioActualizar<Sensor>, IRepositorioEliminar<Sensor>,
                        IRepositorioObtenerPorId<Sensor>, IRepositorioObtenerTodos<Sensor>
    {
    }
}
