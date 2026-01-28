using LogicaDeNegocios.Enums;
using LogicaDeNegocios.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string NumeroTelefono { get; set; }
        public CanalPreferidoNotificacion MedioDeComunicacionDePreferencia { get; set; }
        //no se llama canal porque no puedo repetir el nombre
        public List<Apiario> Apiarios { get; set; } = new List<Apiario>();
        public string NumeroApicultor { get; set; }
        public Estado Estado { get; set; } = Estado.ACTIVA;
        //public string? FotoPerfil { get; set; }

        public Usuario()
        {
        }
        public Usuario( string nombre, string email, string contraseña, string numeroTelefono, string numeroDeApicultor, CanalPreferidoNotificacion medioDeComunicacionDePreferencia)
        {
            this.Nombre = nombre;
            this.Email = email;
            this.Contraseña = contraseña;
            this.NumeroTelefono = numeroTelefono;
            this.NumeroApicultor = numeroDeApicultor;
            this.MedioDeComunicacionDePreferencia = medioDeComunicacionDePreferencia;
        }
        public void ValidarUsuario()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Email) 
                || string.IsNullOrEmpty(Contraseña))
            {
                throw new UsuarioException("Todos los campos son obligatorios.");
            }
            if (!ValidarEmail())
            {
                throw new UsuarioException("El email no es válido.");
            }
            if (Contraseña.Length < 6)
            {
                throw new UsuarioException("La contraseña debe tener al menos 6 caracteres.");
            }
            if (!ValidarTelefono())
            {
                throw new UsuarioException("El número de teléfono debe estar en formato telefonico internacional" +
                    " (ej: +59899123456).");
            }
        }

        private bool ValidarTelefono()
        {
            if (string.IsNullOrWhiteSpace(NumeroTelefono))
            {
                return false;
            }
            // para que funcione con twilio debe cumplir con el formato E.164:
            // - Empezar con +
            // - Seguido de 1-15 digitos
            // - Primer digito después del + no puede ser un 0
            string pattern = @"^\+[1-9][0-9]{0,14}$";
            return Regex.IsMatch(NumeroTelefono, pattern);
        }

        private bool ValidarEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }
            // Regex que valida:
            // - Exactamente un @
            // - Termina en punto seguido de 2 o 3 letras
            string pattern = @"^[^@]+@[^@]+\.[a-zA-Z]{2,3}$";
            // Verificar que cumple con el patrón básico
            if (!Regex.IsMatch(Email, pattern))
            {
                return false;
            }
            int arrobaCount = Email.Count(c => c == '@');
            if (arrobaCount != 1)
            {
                return false;
            }
            return true;
        }
    }
}

