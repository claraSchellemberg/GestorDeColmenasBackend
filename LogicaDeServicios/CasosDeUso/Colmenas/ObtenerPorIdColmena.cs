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
    public class ObtenerPorIdColmena: IObtenerPorId<ColmenaGetDto>
    {
        private IRepositorioColmena _repo;
        public ObtenerPorIdColmena(IRepositorioColmena repo)
        {
            _repo = repo;
        }
        public ColmenaGetDto ObtenerPorId(int id)
        {
            return ColmenaMapper.ToDto(_repo.ObtenerElementoPorId(id));

        }
    }
}
