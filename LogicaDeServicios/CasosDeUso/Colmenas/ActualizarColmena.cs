using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.DTOs.Colmenas;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Colmenas
{
    public class ActualizarColmena: IActualizar<ColmenaSetDto>
    {
        private IRepositorioColmena _repo;
        public ActualizarColmena(IRepositorioColmena repo)
        {
            _repo = repo;
        }
        public void Actualizar(int id, ColmenaSetDto colmenaSetDto)
        {
            Colmena colmena= _repo.ObtenerElementoPorId(id);
            _repo.Actualizar(Mappers.ColmenaMapper.UpdateFromDto(colmena, colmenaSetDto));
        }
    }
}
