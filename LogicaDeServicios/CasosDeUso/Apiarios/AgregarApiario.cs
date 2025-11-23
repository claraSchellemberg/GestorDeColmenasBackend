using LogicaDeNegocios.InterfacesRepositorio;
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
    //aca se llama alos repos que necesito agregar 
    //primero iria a bucar la colmena
    public class AgregarApiario: IAgregar<ApiarioSetDto>
    {
        private IRepositorioApiario _repo;

        public AgregarApiario(IRepositorioApiario repo) 
        {
            _repo = repo; 
        }  

        public void Agregar(ApiarioSetDto apiarioSetDto)
        {
            _repo.Agregar(ApiarioMapper.FromDto(apiarioSetDto));
        }

    }
}
