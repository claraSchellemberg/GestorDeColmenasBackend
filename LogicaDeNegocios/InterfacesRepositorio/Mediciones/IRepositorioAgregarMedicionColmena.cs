using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.Mediciones
{
    public interface IRepositorioAgregarMedicionColmena
    {
        void AgregarMedicion(MedicionColmena medicion, Colmena colmena);
    }
}
