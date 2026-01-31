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
    public class ObtenerApiarioPorIdUsuario : IObtenerPorIdUsuario<IEnumerable<ApiarioGetDto>>
    {
        private IRepositorioApiario _repo;
        public ObtenerApiarioPorIdUsuario(IRepositorioApiario repo)
        {
            _repo = repo;
        }
        public IEnumerable<ApiarioGetDto> ObtenerPorIdUsuario(int idUsuario)
        {
            return ApiarioMapper.ToListDto(_repo.ObtenerElementoPorIdUsuario(idUsuario));
        }

    }
}
