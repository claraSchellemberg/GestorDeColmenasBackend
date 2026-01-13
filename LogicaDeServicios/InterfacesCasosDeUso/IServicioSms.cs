using System.Threading.Tasks;

namespace LogicaDeServicios.InterfacesCasosDeUso
{
    public interface IServicioSms
    {
        Task EnviarAsync(string numeroDestino, string mensaje);
    }
}
