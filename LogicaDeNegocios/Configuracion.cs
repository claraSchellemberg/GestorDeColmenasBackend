using System;
using System.Collections.Generic;

namespace LogicaDeNegocios
{
    public class Configuracion
    {
        private static Dictionary<string, string> _valores = new Dictionary<string, string>();

        // Keep these for EF Core entity mapping
        public string Nombre { get; set; }
        public string Valor { get; set; }

        public Configuracion() { }

        public static void Inicializar(IEnumerable<(string Nombre, string Valor)> configuraciones)
        {
            _valores.Clear();
            foreach (var config in configuraciones)
            {
                _valores[config.Nombre] = config.Valor;
            }
        }

        public static string GetValorPorNombre(string nombre)
        {
            if (_valores.TryGetValue(nombre, out string valor))
            {
                return valor;
            }
            else
            {
                throw new Exception($"Configuración '{nombre}' no encontrada.");
            }
        }
    }
}