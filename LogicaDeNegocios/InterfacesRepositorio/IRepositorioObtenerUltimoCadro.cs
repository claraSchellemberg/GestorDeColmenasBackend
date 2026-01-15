using LogicaDeNegocios.Entidades;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioObtenerUltimoCadro
    {
        RegistroSensor ObtenerUltimoPorCuadro(int cuadroId);
    }
}