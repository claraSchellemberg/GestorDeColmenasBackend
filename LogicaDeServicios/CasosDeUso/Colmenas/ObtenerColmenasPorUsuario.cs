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
    public class ObtenerColmenasPorUsuario : IObtenerColmenasPorUsuario <ColmenaGetDto>
    {
        private IRepositorioColmena _repoColmenas;

        public ObtenerColmenasPorUsuario(IRepositorioColmena repositorioColmena)
        {
            _repoColmenas = repositorioColmena;
        }
        public IEnumerable<ColmenaGetDto> ObtenerColmenasPorUsuarioCU(int usuarioId)
        {
            if (usuarioId != 0)
            {
                var colmenas = _repoColmenas.ObtenerColmenasPorUsuario(usuarioId);
                return ColmenaMapper.ToListDto(colmenas);
            }
            else
            {
                throw new Exception($"el numero de usuario: {usuarioId}, no es correcto");
            }

        }
    }
}
