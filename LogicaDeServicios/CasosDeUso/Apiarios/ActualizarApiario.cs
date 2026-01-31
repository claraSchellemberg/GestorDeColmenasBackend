using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeServicios.DTOs.Apiarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Apiarios
{
    public class ActualizarApiario: IActualizar<ApiarioSetDto>
    {
        private IRepositorioApiario _repo;
        public ActualizarApiario(IRepositorioApiario repo)
        {
            _repo = repo;
        }
        public void Actualizar(int id, ApiarioSetDto apiarioSetDto)
        {
            Apiario apiario= _repo.ObtenerElementoPorId(id);
            _repo.Actualizar(Mappers.ApiarioMapper.UpdateFromDto(apiario, apiarioSetDto));
        }
    }
}
