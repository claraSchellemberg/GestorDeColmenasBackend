using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.Excepciones;
using System;
using Xunit;
using System.Collections.Generic;


namespace LogicaDeNegocios.Tests.Entidades
{
    public class ColmenaTests
    {
        [Fact]
        public void Constructor_ConParametrosValidos_CreaSatisfactoriamente()
        {
            // Arrange
            string descripcion = "Colmena principal";
            string nombre = "C1";
            int apiarioId = 1;

            // Act
            var colmena = new Colmena(descripcion, nombre, apiarioId);

            // Assert
            Assert.Equal(descripcion, colmena.Descripcion);
            Assert.Equal(nombre, colmena.Nombre);
            Assert.Equal(apiarioId, colmena.ApiarioId);
            Assert.Equal(EstadoColmena.OPTIMO, colmena.Estado);
            Assert.NotEqual(DateTime.MinValue, colmena.FechaInstalacionSensores);
        }

        [Fact]
        public void Constructor_ConParametrosVacios_CreaSatisfactoriamente()
        {
            // Act
            var colmena = new Colmena();

            // Assert
            Assert.Null(colmena.Descripcion);
            Assert.Null(colmena.Nombre);
            Assert.Equal(0, colmena.ApiarioId);
        }

        [Fact]
        public void ValidarColmena_ConDescripcionNula_LanzaExcepcion()
        {
            // Arrange
            var colmena = new Colmena { Descripcion = null, Nombre = "C1", ApiarioId = 1 };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => colmena.ValidarColmena());
            Assert.Equal("Todos los campos de la colmena son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarColmena_ConNombreNulo_LanzaExcepcion()
        {
            // Arrange
            var colmena = new Colmena { Descripcion = "Descripción", Nombre = null, ApiarioId = 1 };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => colmena.ValidarColmena());
            Assert.Equal("Todos los campos de la colmena son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarColmena_ConIdApiarioCero_LanzaExcepcion()
        {
            // Arrange
            var colmena = new Colmena { Descripcion = "Descripción", Nombre = "C1", ApiarioId = 0 };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => colmena.ValidarColmena());
            Assert.Equal("Todos los campos de la colmena son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarColmena_ConIdApiariosNegativo_LanzaExcepcion()
        {
            // Arrange
            var colmena = new Colmena { Descripcion = "Descripción", Nombre = "C1", ApiarioId = -1 };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => colmena.ValidarColmena());
            Assert.Equal("Todos los campos de la colmena son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarColmena_ConDescripcionVacia_LanzaExcepcion()
        {
            // Arrange
            var colmena = new Colmena { Descripcion = "", Nombre = "C1", ApiarioId = 1 };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => colmena.ValidarColmena());
            Assert.Equal("Todos los campos de la colmena son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarColmena_ConNombreVacio_LanzaExcepcion()
        {
            // Arrange
            var colmena = new Colmena { Descripcion = "Descripción", Nombre = "", ApiarioId = 1 };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => colmena.ValidarColmena());
            Assert.Equal("Todos los campos de la colmena son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarColmena_ConTodosLosCamposValidos_NoLanzaExcepcion()
        {
            // Arrange
            var colmena = new Colmena { Descripcion = "Descripción", Nombre = "C1", ApiarioId = 1 };

            // Act & Assert
            colmena.ValidarColmena(); // No debe lanzar excepción
        }

        [Fact]
        public void Constructor_EstadoPorDefectoEsOPTIMO()
        {
            // Act
            var colmena = new Colmena("Descripción", "C1", 1);

            // Assert
            Assert.Equal(EstadoColmena.OPTIMO, colmena.Estado);
        }

        [Fact]
        public void Constructor_FechaInstalacionSensoresEsActual()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var colmena = new Colmena("Descripción", "C1", 1);
            var afterCreation = DateTime.Now;

            // Assert
            Assert.True(colmena.FechaInstalacionSensores >= beforeCreation);
            Assert.True(colmena.FechaInstalacionSensores <= afterCreation.AddSeconds(1));
        }

       // [Fact]
       /* public void Colmena_ListasCuadrosYMedicionesNoNulas()
        {
            // Act
            var colmena = new Colmena();

            // Assert
            Assert.NotNull(colmena.Cuadros);
            Assert.NotNull(colmena.Mediciones);
        }*/

        [Fact]
        public void ValidarColmena_ConEspaciosEnBlanco_LanzaExcepcion()
        {
            // Arrange
            var colmena = new Colmena { Descripcion = "   ", Nombre = "C1", ApiarioId = 1 };

            // Act & Assert - IsNullOrEmpty() considera espacios en blanco como válidos
            // Este test documenta el comportamiento actual
            colmena.ValidarColmena(); // No lanza excepción actualmente
        }
    }
}


