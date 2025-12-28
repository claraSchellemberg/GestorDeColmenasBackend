using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios
{
    public class Configuracion
    {
        private static Configuracion _instancia;

        public string Nombre { get; set; }
        public string Valor { get; set; }

        private Configuracion(string nombre, string valor)
        {
            Nombre = nombre;
            Valor = valor;
        }

        public static Configuracion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    throw new Exception("Configuración no inicializada.");
                }
                return _instancia;
            }
        }

        public static void Inicializar(string nombre, string valor)
        {
            if (_instancia == null)
            {
                _instancia = new Configuracion(nombre, valor);
                _instancia.validarCampos();
            }
        }

        public void actualizarValor(string nuevoValor)
        {
            Valor = nuevoValor;
        }

        public void validarCampos()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Valor))
            {
                throw new Exception("Todos los campos son obligatorios.");
            }
        }

        public string GetValorPorNombre(string nombre)
        {
            if (nombre == Nombre)
            {
                return Valor;
            }
            else
            {
                throw new Exception("Configuración no encontrada.");
            }
        }
    }
}
