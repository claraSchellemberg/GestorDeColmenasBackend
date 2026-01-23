using LogicaDeNegocios.Entidades;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioObtenerUltimoCuadro
    {
        RegistroSensor ObtenerUltimoPorCuadro(int cuadroId);
    }
}