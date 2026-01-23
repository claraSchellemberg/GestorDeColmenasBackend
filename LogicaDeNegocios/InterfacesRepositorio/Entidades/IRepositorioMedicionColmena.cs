using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.Entidades
{
    public interface IRepositorioMedicionColmena: IRepositorioAgregar<MedicionColmena>,
                        IRepositorioActualizar<MedicionColmena>, IRepositorioEliminar<MedicionColmena>,
                        IRepositorioObtenerPorId<MedicionColmena>, IRepositorioObtenerTodos<MedicionColmena>
    {
    }
}
