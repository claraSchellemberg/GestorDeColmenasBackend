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
    public class AgregarRegistro: IAgregar<RegistroSetDto>
    {
        private IRepositorioRegistro _repo;

        public AgregarRegistro(IRepositorioRegistro repo)
        {
            _repo = repo;
        }

        public void Agregar(RegistroSetDto registroSetDto)
        {
            _repo.Agregar(RegistroMapper.FromDto(registroSetDto));
        }
    }
}
