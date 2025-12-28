using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioRegistroMedicionColmena: IRepositorioAgregar<RegistroMedicionColmena>,
                        IRepositorioActualizar<RegistroMedicionColmena>, IRepositorioEliminar<RegistroMedicionColmena>,
                        IRepositorioObtenerPorId<RegistroMedicionColmena>, IRepositorioObtenerTodos<RegistroMedicionColmena>
    {
    }
}
