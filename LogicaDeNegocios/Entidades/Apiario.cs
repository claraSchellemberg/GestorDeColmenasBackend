using LogicaDeNegocios.Enums;
using LogicaDeNegocios.Excepciones;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogicaDeNegocios.Entidades
{
    public class Apiario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string UbicacionDeReferencia { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now;
        public Estado Estado { get; set; } = Estado.ACTIVA;
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Colmena> Colmenas { get; set; } = new List<Colmena>();
        public Apiario() { }
        
        public Apiario(string nombre, string latitud, string longitud, string ubicacion,
                        int usuarioId)
        {
            this.Nombre = nombre;
            this.UbicacionDeReferencia = ubicacion;
            this.Latitud = latitud;
            this.Longitud = longitud;
            this.UsuarioId = usuarioId;
        }
        public void AgregarColmena(Colmena colmena)
        {
            Colmenas.Add(colmena);
        }
        public void ValidarApiario()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Latitud) ||
                string.IsNullOrEmpty(Longitud) || string.IsNullOrEmpty(UbicacionDeReferencia))
            {
                throw new ApiarioException("Todos los campos del apiario son obligatorios.");
            }
            if (!ValidarLatitud())
            {
                throw new ApiarioException("La latitud debe estar en formato decimal entre -90 y 90 (ej: -34.9011).");
            }
            if (!ValidarLongitud())
            {
                throw new ApiarioException("La longitud debe estar en formato decimal entre -180 y 180 (ej: -56.1645).");
            }
        }

        private bool ValidarLatitud()
        {
            if (string.IsNullOrWhiteSpace(Latitud))
            {
                return false;
            }
            // Regex para latitud: -90 a 90, formato decimal
            string pattern = @"^-?([0-8]?[0-9](\.\d+)?|90(\.0+)?)$";
            return Regex.IsMatch(Latitud.Trim(), pattern);
        }

        private bool ValidarLongitud()
        {
            if (string.IsNullOrWhiteSpace(Longitud))
            {
                return false;
            }
            // Regex para longitud: -180 a 180, formato decimal
            string pattern = @"^-?(1[0-7][0-9](\.\d+)?|180(\.0+)?|[0-9]{1,2}(\.\d+)?)$";
            return Regex.IsMatch(Longitud.Trim(), pattern);
        }
    }
}
