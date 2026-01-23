using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using LogicaDeNegocios.InterfacesRepositorio.Mediciones;

namespace LogicaDeNegocios.InterfacesRepositorio.Entidades
{
    public interface IRepositorioCuadro : IRepositorioAgregar<Cuadro>, IRepositorioActualizar<Cuadro>,
                                        IRepositorioEliminar<Cuadro>, IRepositorioObtenerPorId<Cuadro>,
                                            IRepositorioObtenerTodos<Cuadro>, IRepositorioAgregarMedicionPorCuadro
    {
    }
}
