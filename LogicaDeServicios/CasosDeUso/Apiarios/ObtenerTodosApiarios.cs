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
    public class ObtenerTodosApiarios: IObtenerTodos<ApiarioGetDto>
    {
        private IRepositorioApiario _repo;

        public ObtenerTodosApiarios(IRepositorioApiario repo)
        {
            _repo = repo;
        }

        public IEnumerable<ApiarioGetDto> ObtenerTodos()
        {
            return ApiarioMapper.ToListDto(_repo.ObtenerTodosLosElementos());
        }
    }
}
