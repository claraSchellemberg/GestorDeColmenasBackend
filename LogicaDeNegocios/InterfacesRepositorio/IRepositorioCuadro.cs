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
    public interface IRepositorioCuadro : IRepositorioAgregar<Cuadro>, IRepositorioActualizar<Cuadro>,
                                        IRepositorioEliminar<Cuadro>, IRepositorioObtenerPorId<Cuadro>,
                                            IRepositorioObtenerTodos<Cuadro>, IRepositorioAgregarMedicionPorCuadro
=======
    public interface IRepositorioCuadro: IRepositorioAgregar<Cuadro>,
                        IRepositorioActualizar<Cuadro>, IRepositorioEliminar<Cuadro>,
                        IRepositorioObtenerPorId<Cuadro>, IRepositorioObtenerTodos<Cuadro>
>>>>>>> origin/Develop
    {
    }
}
