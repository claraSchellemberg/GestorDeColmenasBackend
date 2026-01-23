using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.Registros
{
    public interface IRepositorioRegistroMedicionColmena: IRepositorioAgregar<RegistroMedicionColmena>,
                    IRepositorioObtenerPorId<RegistroMedicionColmena>, 
                        IRepositorioObtenerTodos<RegistroMedicionColmena>,
                        IRepositorioObtenerRegistrosPorColmena<RegistroMedicionColmena>
    {
    }
}
