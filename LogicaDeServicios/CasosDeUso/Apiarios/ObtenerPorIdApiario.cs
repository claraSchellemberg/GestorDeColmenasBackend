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
    public class ObtenerPorIdApiario: IObtenerPorId<ApiarioGetDto>
    {
        private IRepositorioApiario _repo;

        public ObtenerPorIdApiario(IRepositorioApiario repo)
        {
            _repo = repo;
        }  
        
        public ApiarioGetDto ObtenerPorId(int id)
        {
            return ApiarioMapper.ToDto(_repo.ObtenerElementoPorId(id));
        }
    }
}
