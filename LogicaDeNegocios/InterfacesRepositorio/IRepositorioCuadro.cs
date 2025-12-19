using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioCuadro : IRepositorioAgregar<Cuadro>, IRepositorioActualizar<Cuadro>,
                                        IRepositorioEliminar<Cuadro>, IRepositorioObtenerPorId<Cuadro>,
                                            IRepositorioObtenerTodos<Cuadro>, IRepositorioAgregarMedicionPorCuadro
    {
    }
}
