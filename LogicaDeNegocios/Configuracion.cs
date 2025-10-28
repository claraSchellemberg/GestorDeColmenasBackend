using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
    public class Configuracion
    {
        public string NombreIdentificador { get; set; }
        public string Valor { get; set; }
        public Configuracion(string nombreIdentificador, string valor)
        {
            NombreIdentificador = nombreIdentificador;
            Valor = valor;
        }
        public void actualizarValor(string nuevoValor)
        {
            Valor = nuevoValor;
        }
        public void validarCampos()
        {
            if (string.IsNullOrEmpty(NombreIdentificador) || string.IsNullOrEmpty(Valor))
            {
                throw new Exception("Todos los campos son obligatorios.");
            }
        }

    }
}
