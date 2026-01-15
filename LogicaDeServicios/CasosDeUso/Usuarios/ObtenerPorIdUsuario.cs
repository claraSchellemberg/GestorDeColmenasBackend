using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.DTOs.Usuarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using LogicaDeServicios.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Usuarios
{
    public class ObtenerPorIdUsuario: IObtenerPorId<UsuarioGetDto>
    {
        private IRepositorioUsuario _repo;
        public ObtenerPorIdUsuario(IRepositorioUsuario repo)
        {
            _repo = repo;
        }
        public UsuarioGetDto ObtenerPorId(int id)
        {
            return UsuarioMapper.ToDto(_repo.ObtenerElementoPorId(id));
        }

    }
}
