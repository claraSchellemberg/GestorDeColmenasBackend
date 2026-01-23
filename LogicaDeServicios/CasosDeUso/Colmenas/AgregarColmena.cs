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
    public class AgregarColmena: IAgregar<ColmenaSetDto, ColmenaGetDto>
    {
        private IRepositorioColmena _repoColmena;
        private IRepositorioApiario _repoApiario;
        public AgregarColmena(IRepositorioColmena repoColmena, IRepositorioApiario repoApiario)
        {
            _repoColmena = repoColmena;
            _repoApiario = repoApiario;
        }

        public ColmenaGetDto Agregar(ColmenaSetDto colmenaSetDto)
        {
            //validar que el apiario exista
            var apiario = _repoApiario.ObtenerElementoPorId(colmenaSetDto.ApiarioId);
            if (apiario == null)
            {
                throw new ColmenaException($"El apiario con ID {colmenaSetDto.ApiarioId} no existe.");
            }
            ColmenaGetDto agregada;
            agregada = ColmenaMapper.ToDto(
                _repoColmena.Agregar(ColmenaMapper.FromDto(colmenaSetDto))
                );
            return agregada;
        }
    }
}
