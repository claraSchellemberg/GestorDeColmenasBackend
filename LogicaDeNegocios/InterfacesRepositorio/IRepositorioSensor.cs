using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioSensor : IRepositorioAgregar<Sensor>,
                        IRepositorioActualizar<Sensor>, IRepositorioEliminar<Sensor>,
                        IRepositorioObtenerPorId<Sensor>, IRepositorioObtenerTodos<Sensor>
    {
    }
}
