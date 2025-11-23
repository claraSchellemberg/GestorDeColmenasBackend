using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using LogicaDeServicios.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.TomarMedicion
{
   // public class AgregarRegistro: IAgregar<RegistroSetDto>

        //aca agrega a los sesores por cuadro y al otro donde se agrega el peso y temp externa
        //casos de usos son el negocio

        //con el registrosetdto(aca trae el id de sensor que viene hardcodeado) ir a buscar la colmena, me devuelve la placa que corresponde ese sensor
        //aca agrego nuevo csensor por cuador y cambio peso de colmena llamo a los reepo correspondientes (colmena y sensor por cuadro)
        //en el caso de uso llamo a todos los repos necesarios y los repos guardan en la base de datos 
    //{
        //private IRepositorioRegistro _repo;

        //public AgregarRegistro(IRepositorioRegistro repo)
        //{
        //    _repo = repo;
        //}

        //public void Agregar(RegistroSetDto registroSetDto)
        //{
        //    _repo.Agregar(RegistroMapper.FromDto(registroSetDto));
        //}
    //}
}
