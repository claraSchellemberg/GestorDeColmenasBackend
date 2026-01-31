using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeServicios.DTOs.Colmenas;
using LogicaDeServicios.InterfacesCasosDeUso;
using LogicaDeServicios.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Colmenas
{
    public class ObtenerTodosColmenas: IObtenerTodos<ColmenaGetDto>
    {
        private IRepositorioColmena _repo;
        public ObtenerTodosColmenas(IRepositorioColmena repo)
        {
            _repo = repo;
        }
        public IEnumerable<ColmenaGetDto> ObtenerTodos()
        {
            return ColmenaMapper.ToListDto(_repo.ObtenerTodosLosElementos());
        }
    }
}
