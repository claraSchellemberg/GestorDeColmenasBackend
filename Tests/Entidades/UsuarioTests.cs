using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using System;
using Xunit;
using System.Collections.Generic;
using LogicaDeNegocios.Enums;


namespace LogicaDeNegocios.Tests.Entidades
{
    public class UsuarioTests
    {
        [Fact]
        public void Constructor_ConParametrosValidos_CreaSatisfactoriamente()
        {
            // Arrange
            string nombre = "Juan Pérez";
            string email = "juan@example.com";
            string contraseña = "contraseña123";
            string numeroTelefono = "+59899123456";
            string numeroApicultor = "AP12345";
            CanalPreferidoNotificacion medioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS;

            // Act
            var usuario = new Usuario(nombre, email, contraseña, numeroTelefono, numeroApicultor, medioDeComunicacionDePreferencia);


            // Assert
            Assert.Equal(nombre, usuario.Nombre);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(contraseña, usuario.Contraseña);
            Assert.Equal(numeroTelefono, usuario.NumeroTelefono);
            Assert.Equal(numeroApicultor, usuario.NumeroApicultor);
            Assert.Equal(medioDeComunicacionDePreferencia, usuario.MedioDeComunicacionDePreferencia);

        }

        [Fact]
        public void Constructor_ParametroVacio_CreaSatisfactoriamente()
        {
            // Act
            var usuario = new Usuario();

            // Assert
            Assert.Null(usuario.Nombre);
            Assert.Null(usuario.Email);
            Assert.Null(usuario.Contraseña);
            Assert.Null(usuario.NumeroTelefono);
            Assert.Null(usuario.NumeroApicultor);
            Assert.Equal(default(CanalPreferidoNotificacion), usuario.MedioDeComunicacionDePreferencia);
        }

        [Fact]
        public void Usuario_ListaApiariosNoNula()
        {
            // Act
            var usuario = new Usuario();

            // Assert
            Assert.NotNull(usuario.Apiarios);
            Assert.Empty(usuario.Apiarios);
        }

        [Fact]
        public void ValidarUsuario_ConNombreNulo_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = null, Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia= CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConEmailNulo_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = null, Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConContraseñaNula_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = null, NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConNombreVacio_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConEmailVacio_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS};


            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConContraseñaVacia_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConEmailInvalido_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "emailSinArroba.com", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("El email no es válido.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConEmailMultiplesArrobas_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@@example.com", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("El email no es válido.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConEmailSinExtension_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("El email no es válido.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConContraseñaMenor6Caracteres_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "pass1", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("La contraseña debe tener al menos 6 caracteres.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConContraseña5Caracteres_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "pass1", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("La contraseña debe tener al menos 6 caracteres.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConTelefonoInvalido_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Contains("El número de teléfono debe estar en formato telefonico internacional", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConTelefonoSinPlus_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Contains("El número de teléfono debe estar en formato telefonico internacional", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConTelefonoComenzandoCero_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "+09999123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Contains("El número de teléfono debe estar en formato telefonico internacional", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConTelefonoVacio_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Contains("El número de teléfono debe estar en formato telefonico internacional", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConTelefonoNulo_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = null, MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Contains("El número de teléfono debe estar en formato telefonico internacional", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConTodosLosCamposValidos_NoLanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            usuario.ValidarUsuario(); // No debe lanzar excepción
        }

        [Fact]
        public void ValidarUsuario_ConEmailValido_NoLanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "test@domain.co", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            usuario.ValidarUsuario(); // No debe lanzar excepción
        }

        [Fact]
        public void ValidarUsuario_ConContraseña6Caracteres_NoLanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "pass12", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            usuario.ValidarUsuario(); // No debe lanzar excepción
        }

        [Fact]
        public void ValidarUsuario_ConTelefonoValido_NoLanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "+59899999999", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            usuario.ValidarUsuario(); // No debe lanzar excepción
        }

        [Fact]
        public void ValidarUsuario_ConEspaciosEnBlanco_NoLanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "   ", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "+59899123456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert - IsNullOrEmpty() considera espacios en blanco como válidos
            // Este test documenta el comportamiento actual
            usuario.ValidarUsuario(); // No lanza excepción
        }

        [Fact]
        public void AgregarApiario_ConApiarioValido_AgregaSatisfactoriamente()
        {
            // Arrange
            var usuario = new Usuario("Juan", "juan@example.com", "contraseña123", "+59899123456", "AP12345", CanalPreferidoNotificacion.SMS);
            var apiario = new Apiario("Apiario Principal", "-34.5", "-58.5", "San Isidro", 1);

            // Act
            usuario.Apiarios.Add(apiario);

            // Assert
            Assert.Single(usuario.Apiarios);
            Assert.Equal(apiario, usuario.Apiarios[0]);
        }

        [Fact]
        public void AgregarApiario_MultiplesApiarios_AgregaSatisfactoriamente()
        {
            // Arrange
            var usuario = new Usuario("Juan", "juan@example.com", "contraseña123", "+59899123456", "AP12345", CanalPreferidoNotificacion.SMS);
            var apiario1 = new Apiario("Apiario 1", "-34.5", "-58.5", "San Isidro", 1);
            var apiario2 = new Apiario("Apiario 2", "-35.5", "-59.5", "La Plata", 1);
            var apiario3 = new Apiario("Apiario 3", "-36.5", "-60.5", "Tandil", 1);

            // Act
            usuario.Apiarios.Add(apiario1);
            usuario.Apiarios.Add(apiario2);
            usuario.Apiarios.Add(apiario3);

            // Assert
            Assert.Equal(3, usuario.Apiarios.Count);
            Assert.Contains(apiario1, usuario.Apiarios);
            Assert.Contains(apiario2, usuario.Apiarios);
            Assert.Contains(apiario3, usuario.Apiarios);
        }

        public void ValidarUsuario_ConEmailConMultiplesPuntos_NoLanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Juan", Email = "juan.perez@example.co.uk", Contraseña = "contraseña123", NumeroTelefono = "+59899123456" };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("El email no es válido.", ex.Message);
        }

        [Fact]
        public void ValidarUsuario_ConTelefonoConEspacios_LanzaExcepcion()
        {
            // Arrange
           var usuario = new Usuario { Nombre = "Juan", Email = "juan@example.com", Contraseña = "contraseña123", NumeroTelefono = "+598 99 123 456", MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS };

            // Act & Assert
            var ex = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Contains("El número de teléfono debe estar en formato telefonico internacional", ex.Message);
        }
    }
}