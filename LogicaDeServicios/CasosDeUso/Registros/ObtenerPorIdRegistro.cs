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
    public class ObtenerPorIdRegistro : IObtenerPorId<RegistroGetDto>
    {
        private IRepositorioRegistro _repo;

        public ObtenerPorIdRegistro(IRepositorioRegistro repo) 
        {
            _repo = repo;
        } 

        public RegistroGetDto ObtenerPorId(int id)
        {
            return RegistroMapper.ToDto(_repo.ObtenerElementoPorId(id));
        }
    }
}
