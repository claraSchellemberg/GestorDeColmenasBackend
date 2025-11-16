using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using NUnit.Framework;
using System;
using Xunit;

namespace LogicaDeNegocios.Tests.Entidades
{
    public class ApiarioTests
    {
        [Fact]
        public void Constructor_ConDatosValidos_CreaApiarioCorrectamente()
        {
            // Arrange
            var nombre = "Apiario Norte";
            var latitud = "-34.9011";
            var longitud = "-56.1645";
            var ubicacion = "Montevideo, Uruguay";

            // Act
            var apiario = new Apiario(nombre, latitud, longitud, ubicacion);

            // Assert
            Xunit.Assert.Equal(nombre, apiario.Nombre);
            Xunit.Assert.Equal(latitud, apiario.Latitud);
            Xunit.Assert.Equal(longitud, apiario.Longitud);
            Xunit.Assert.Equal(ubicacion, apiario.UbicacionDeReferencia);
            Xunit.Assert.True(apiario.Id >= 0);
            Xunit.Assert.NotNull(apiario.Colmenas);
            Xunit.Assert.Empty(apiario.Colmenas);
            Xunit.Assert.True((DateTime.Now - apiario.FechaAlta).TotalSeconds < 2);
        }

        [Fact]
        public void AgregarColmena_ConColmenaValida_AgregaCorrectamente()
        {
            // Arrange
            var apiario = new Apiario("Apiario Test", "-34.9", "-56.1", "Test");
            var colmena = new Colmena("Colmena 1", "Colmena 1");
            //var colmena = new Colmena("Colmena 1", Enums.EstadoColmena.OPTIMO);

            // Act
            apiario.AgregarColmena(colmena);

            // Assert
            Xunit.Assert.Single(apiario.Colmenas);
            Xunit.Assert.Contains(colmena, apiario.Colmenas);
        }

        [Fact]
        public void AgregarColmena_VariasColmenas_AgregaTodasCorrectamente()
        {
            // Arrange
            var apiario = new Apiario("Apiario Test", "-34.9", "-56.1", "Test");
            var colmena1 = new Colmena("Colmena 1", "Colmena 1"); 
            var colmena2 = new Colmena("Colmena 2", "Colmena 2");
            // var colmena1 = new Colmena("Colmena 1", Enums.EstadoColmena.OPTIMO);   //se comentan dado que sacamos el estado que le pasabamos pro parametro del constructor
            //var colmena2 = new Colmena("Colmena 2", Enums.EstadoColmena.NECESITA_REVISION);

            // Act
            apiario.AgregarColmena(colmena1);
            apiario.AgregarColmena(colmena2);

            // Assert
            Xunit.Assert.Equal(2, apiario.Colmenas.Count);
            Xunit.Assert.Contains(colmena1, apiario.Colmenas);
            Xunit.Assert.Contains(colmena2, apiario.Colmenas);
        }

        [Fact]
        public void ValidarApiario_ConDatosValidos_NoLanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario("Apiario Test", "-34.9", "-56.1", "Test");

            // Act & Assert
            var exception = Record.Exception(() => apiario.ValidarApiario());
            Xunit.Assert.Null(exception);
        }

        [Xunit.Theory]
        [InlineData("", "-34.9", "-56.1", "Test")]
        [InlineData(null, "-34.9", "-56.1", "Test")]
        [InlineData("Apiario", "", "-56.1", "Test")]
        [InlineData("Apiario", null, "-56.1", "Test")]
        [InlineData("Apiario", "-34.9", "", "Test")]
        [InlineData("Apiario", "-34.9", null, "Test")]
        [InlineData("Apiario", "-34.9", "-56.1", "")]
        [InlineData("Apiario", "-34.9", "-56.1", null)]
        public void ValidarApiario_ConCamposVacios_LanzaApiarioException(
            string nombre, string latitud, string longitud, string ubicacion)
        {
            // Arrange
            var apiario = new Apiario(nombre, latitud, longitud, ubicacion);

            // Act & Assert
            var exception = Xunit.Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Xunit.Assert.Equal("Todos los campos del apiario son obligatorios.", exception.Message);
        }

        [Fact]
        public void ValidarApiario_ConTodosCamposVacios_LanzaApiarioException()
        {
            // Arrange
            var apiario = new Apiario("", "", "", "");

            // Act & Assert
            var exception = Xunit.Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Xunit.Assert.Equal("Todos los campos del apiario son obligatorios.", exception.Message);
        }
    }
}
