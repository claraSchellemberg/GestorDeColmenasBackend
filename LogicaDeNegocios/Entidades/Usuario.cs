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
        private int UltimoId = 0;
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public List<Apiario> Apiarios { get; set; } = new List<Apiario>();
        public Usuario( string nombre, string email, string contraseña)
        {
            this.Id = UltimoId++;
            this.Nombre = nombre;
            this.Email = email;
            this.Contraseña = contraseña;
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
        }


        public bool ValidarEmail()
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

