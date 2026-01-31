using LogicaDeNegocios.Excepciones;
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
    public class ObtenerColmenasPorApiario: IObtenerColmenasPorApiario<ColmenaGetDto>
    {
        private IRepositorioColmena _repoColmenas;
        private IRepositorioApiario _repoApiarios;

        public ObtenerColmenasPorApiario(IRepositorioColmena repoColmenas, IRepositorioApiario repoApiarios)
        {
            _repoColmenas = repoColmenas;
            _repoApiarios = repoApiarios;
        }

        public IEnumerable<ColmenaGetDto> ObtenerColmenas(int idApiario)
        {
            //primero verifico que el apiario exista
            var apiario = _repoApiarios.ObtenerElementoPorId(idApiario);
            if(apiario == null)
            {
                throw new ApiarioException($"El apiario con Id {idApiario} no existe");
            }
            var colmenas = _repoColmenas.ObtenerColmenasPorApiario(idApiario);
            return ColmenaMapper.ToListDto(colmenas);
        }
    }
}
