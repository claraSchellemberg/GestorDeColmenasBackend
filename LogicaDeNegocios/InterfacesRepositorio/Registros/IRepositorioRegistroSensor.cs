using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.Registros
{
    public interface IRepositorioRegistroSensor: IRepositorioAgregar<RegistroSensor>,
                        IRepositorioObtenerPorId<RegistroSensor>, IRepositorioObtenerTodos<RegistroSensor>, 
                        IRepositorioObtenerUltimoCuadro, IRepositorioObtenerRegistrosPorColmena<RegistroSensor>
    {
    }
}