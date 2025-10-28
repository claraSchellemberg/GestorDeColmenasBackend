using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using Xunit;

namespace LogicaDeNegocios.Tests.Entidades
{
    public class UsuarioTests
    {
        [Fact]
        public void Constructor_ConDatosValidos_CreaUsuarioCorrectamente()
        {
            // Arrange
            var nombre = "Juan Pérez";
            var email = "juan.perez@ejemplo.com";
            var contraseña = "password123";

            // Act
            var usuario = new Usuario(nombre, email, contraseña);

            // Assert
            Assert.Equal(nombre, usuario.Nombre);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(contraseña, usuario.Contraseña);
            Assert.True(usuario.Id >= 0);
            Assert.NotNull(usuario.Apiarios);
            Assert.Empty(usuario.Apiarios);
        }

        [Fact]
        public void ValidarUsuario_ConDatosValidos_NoLanzaExcepcion()
        {
            // Arrange
            var usuario = new Usuario("Juan", "juan@test.com", "password123");

            // Act & Assert
            var exception = Record.Exception(() => usuario.ValidarUsuario());
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("", "test@test.com", "password")]
        [InlineData(null, "test@test.com", "password")]
        public void ValidarUsuario_ConNombreVacioONull_LanzaUsuarioException(
            string nombre, string email, string contraseña)
        {
            // Arrange
            var usuario = new Usuario(nombre, email, contraseña);

            // Act & Assert
            var exception = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", exception.Message);
        }

        [Theory]
        [InlineData("Juan", "", "password")]
        [InlineData("Juan", null, "password")]
        public void ValidarUsuario_ConEmailVacioONull_LanzaUsuarioException(
            string nombre, string email, string contraseña)
        {
            // Arrange
            var usuario = new Usuario(nombre, email, contraseña);

            // Act & Assert
            var exception = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", exception.Message);
        }

        [Theory]
        [InlineData("Juan", "test@test.com", "")]
        [InlineData("Juan", "test@test.com", null)]
        public void ValidarUsuario_ConContraseñaVaciaONull_LanzaUsuarioException(
            string nombre, string email, string contraseña)
        {
            // Arrange
            var usuario = new Usuario(nombre, email, contraseña);

            // Act & Assert
            var exception = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", exception.Message);
        }

        [Theory]
        [InlineData("testtest.com")]
        [InlineData("test.com")]
        [InlineData("usuario")]
        [InlineData("test@@test.com")]
        public void ValidarUsuario_ConEmailSinArroba_LanzaUsuarioException(string emailInvalido)
        {
            // Arrange
            var usuario = new Usuario("Juan", emailInvalido, "password123");

            // Act & Assert
            var exception = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("El email no es válido.", exception.Message);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        public void ValidarUsuario_ConContraseñaMenorA6Caracteres_LanzaUsuarioException(string contraseñaCorta)
        {
            // Arrange
            var usuario = new Usuario("Juan", "juan@test.com", contraseñaCorta);

            // Act & Assert
            var exception = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("La contraseña debe tener al menos 6 caracteres.", exception.Message);
        }

        [Theory]
        [InlineData("123456")]
        [InlineData("1234567")]
        [InlineData("contraseña_muy_larga_123")]
        public void ValidarUsuario_ConContraseña6CaracteresOMas_NoLanzaExcepcion(string contraseñaValida)
        {
            // Arrange
            var usuario = new Usuario("Juan", "juan@test.com", contraseñaValida);

            // Act & Assert
            var exception = Record.Exception(() => usuario.ValidarUsuario());
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("test@ejemplo.com")]
        [InlineData("usuario@dominio.co.uy")]
        [InlineData("nombre.apellido@empresa.com")]
        [InlineData("test123@test.net")]
        public void ValidarUsuario_ConEmailsValidos_NoLanzaExcepcion(string emailValido)
        {
            // Arrange
            var usuario = new Usuario("Juan", emailValido, "password123");

            // Act & Assert
            var exception = Record.Exception(() => usuario.ValidarUsuario());
            Assert.Null(exception);
        }

        [Fact]
        public void Usuario_PuedeAgregarApiarios()
        {
            // Arrange
            var usuario = new Usuario("Juan", "juan@test.com", "password123");
            var apiario = new Apiario("Apiario 1", "-34.9", "-56.1", "Montevideo");

            // Act
            usuario.Apiarios.Add(apiario);

            // Assert
            Assert.Single(usuario.Apiarios);
            Assert.Contains(apiario, usuario.Apiarios);
        }

        [Fact]
        public void Usuario_PuedeAgregarMultiplesApiarios()
        {
            // Arrange
            var usuario = new Usuario("Juan", "juan@test.com", "password123");
            var apiario1 = new Apiario("Apiario 1", "-34.9", "-56.1", "Montevideo");
            var apiario2 = new Apiario("Apiario 2", "-34.8", "-56.2", "Canelones");

            // Act
            usuario.Apiarios.Add(apiario1);
            usuario.Apiarios.Add(apiario2);

            // Assert
            Assert.Equal(2, usuario.Apiarios.Count);
            Assert.Contains(apiario1, usuario.Apiarios);
            Assert.Contains(apiario2, usuario.Apiarios);
        }

        [Fact]
        public void ValidarUsuario_ConTodosCamposVacios_LanzaUsuarioException()
        {
            // Arrange
            var usuario = new Usuario("", "", "");

            // Act & Assert
            var exception = Assert.Throws<UsuarioException>(() => usuario.ValidarUsuario());
            Assert.Equal("Todos los campos son obligatorios.", exception.Message);
        }
    }
}