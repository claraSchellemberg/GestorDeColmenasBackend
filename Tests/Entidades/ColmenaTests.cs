using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using Xunit;
using System;

namespace LogicaDeNegocios.Tests.Entidades
{
    public class ColmenaTests
    {
        [Fact]
        public void Constructor_ConDatosValidos_CreaColmenaCorrectamente()
        {
            // Arrange
            var descripcion = "Colmena producción principal";
            var nombre = "Colmena";
            //var estado = EstadoColmena.OPTIMO;

            // Act
            var colmena = new Colmena(descripcion,nombre);
            //var colmena = new Colmena(descripcion, estado);

            // Assert
            Assert.Equal(descripcion, colmena.Descripcion);
            Assert.Equal(EstadoColmena.OPTIMO, colmena.Estado);
            Assert.True(colmena.Id >= 0);
            Assert.NotNull(colmena.Registros);
            Assert.Empty(colmena.Registros);
            Assert.True((DateTime.Now - colmena.FechaInstalacionSensores).TotalSeconds < 2);
        }

       /* [Theory]
        [InlineData(EstadoColmena.OPTIMO)]
        [InlineData(EstadoColmena.NECESITA_REVISION)]
        public void Constructor_ConDiferentesEstados_AsignaEstadoCorrectamente(EstadoColmena estado)
        {
            // Arrange & Act
            var colmena = new Colmena("Test Colmena");
            //var colmena = new Colmena("Test Colmena", estado);

            // Assert
            Assert.Equal(estado, colmena.Estado);
        }*/

        [Fact]
        public void Colmena_ConDescripcionVacia_SeCreaSinValidacion()
        {
            // Arrange & Act
            var colmena = new Colmena("", "");
            //var colmena = new Colmena("", EstadoColmena.OPTIMO);

            // Assert
            Assert.NotNull(colmena);
            Assert.Equal("", colmena.Descripcion);
        }

        [Fact]
        public void Colmena_ListaRegistros_InicializaVacia()
        {
            // Arrange & Act
            var colmena = new Colmena("Test", "Test");
            //var colmena = new Colmena("Test", EstadoColmena.OPTIMO);

            // Assert
            Assert.NotNull(colmena.Registros);
            Assert.Empty(colmena.Registros);
        }

        [Fact]
        public void Colmena_PuedeAgregarRegistros()
        {
            // Arrange
            var colmena = new Colmena("Test", "Test");
            //var colmena = new Colmena("Test", EstadoColmena.OPTIMO);
            var registro = new Registro("Colmena",30.5f, 31.2f, 29.8f, 25.0f, 45.3f);

            // Act
            colmena.Registros.Add(registro);

            // Assert
            Assert.Single(colmena.Registros);
            Assert.Contains(registro, colmena.Registros);
        }
    }
}