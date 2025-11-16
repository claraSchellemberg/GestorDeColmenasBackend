using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using LogicaDeServicios.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Registros
{
    public class ObtenerTodosRegistros : IObtenerTodos<RegistroGetDto>
    {
        private IRepositorioRegistro _repo;

        public ObtenerTodosRegistros(IRepositorioRegistro repo)
        {
            _repo = repo;
        }

        public IEnumerable<RegistroGetDto> ObtenerTodos()
        {
            return RegistroMapper.ToListDto(_repo.ObtenerTodosLosElementos());
        }
    }
}
