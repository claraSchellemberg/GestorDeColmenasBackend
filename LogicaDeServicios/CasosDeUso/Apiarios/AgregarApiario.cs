using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeServicios.DTOs.Apiarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using LogicaDeServicios.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Apiarios
{
    //aca se llama a los repos que necesito agregar 
    //primero iria a bucar la colmena
    public class AgregarApiario: IAgregar<ApiarioSetDto, ApiarioGetDto>
    {
        private IRepositorioApiario _repo;

        public AgregarApiario(IRepositorioApiario repo) 
        {
            _repo = repo; 
        }  

        public ApiarioGetDto Agregar(ApiarioSetDto apiarioSetDto)
        {
            ApiarioGetDto agregado;
            agregado =ApiarioMapper.ToDto( 
                _repo.Agregar(ApiarioMapper.FromDto(apiarioSetDto))
                );
            return agregado;
        }

    }
}
