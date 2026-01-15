using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using System;
using Xunit;
using System.Collections.Generic;


namespace LogicaDeNegocios.Tests.Entidades
{
    public class ApiarioTests
    {
        [Fact]
        public void Constructor_ConParametrosValidos_CreaSatisfactoriamente()
        {
            // Arrange
            string nombre = "Apiario Principal";
            string latitud = "-34.5";
            string longitud = "-58.5";
            string ubicacion = "San Isidro";
            int usuarioId = 1;

            // Act
            var apiario = new Apiario(nombre, latitud, longitud, ubicacion, usuarioId);

            // Assert
            Assert.Equal(nombre, apiario.Nombre);
            Assert.Equal(latitud, apiario.Latitud);
            Assert.Equal(longitud, apiario.Longitud);
            Assert.Equal(ubicacion, apiario.UbicacionDeReferencia);
        }

        [Fact]
        public void Constructor_ParametroVacio_CreaSatisfactoriamente()
        {
            // Act
            var apiario = new Apiario();

            // Assert
            Assert.Null(apiario.Nombre);
            Assert.Null(apiario.Latitud);
            Assert.Null(apiario.Longitud);
            Assert.Null(apiario.UbicacionDeReferencia);
        }

        [Fact]
        public void Constructor_FechaAltaPorDefecto_EsLaFechaActual()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var apiario = new Apiario("Nombre", "-34.5", "-58.5", "Ubicacion", 1);
            var afterCreation = DateTime.Now;

            // Assert
            Assert.True(apiario.FechaAlta >= beforeCreation);
            Assert.True(apiario.FechaAlta <= afterCreation.AddSeconds(1));
        }

        [Fact]
        public void ValidarApiario_ConNombreNulo_LanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = null, Latitud = "-34.5", Longitud = "-58.5", UbicacionDeReferencia = "Ubicacion" };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Assert.Equal("Todos los campos del apiario son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarApiario_ConLatitudNula_LanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "Apiario", Latitud = null, Longitud = "-58.5", UbicacionDeReferencia = "Ubicacion" };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Assert.Equal("Todos los campos del apiario son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarApiario_ConLongitudNula_LanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "Apiario", Latitud = "-34.5", Longitud = null, UbicacionDeReferencia = "Ubicacion" };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Assert.Equal("Todos los campos del apiario son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarApiario_ConUbicacionNula_LanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "Apiario", Latitud = "-34.5", Longitud = "-58.5", UbicacionDeReferencia = null };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Assert.Equal("Todos los campos del apiario son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarApiario_ConTodosLosCamposValidos_NoLanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "Apiario", Latitud = "-34.5", Longitud = "-58.5", UbicacionDeReferencia = "Ubicacion" };

            // Act & Assert
            apiario.ValidarApiario(); // No debe lanzar excepción
        }

        [Fact]
        public void ValidarApiario_ConNombreVacio_LanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "", Latitud = "-34.5", Longitud = "-58.5", UbicacionDeReferencia = "Ubicacion" };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Assert.Equal("Todos los campos del apiario son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarApiario_ConLatitudVacia_LanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "Apiario", Latitud = "", Longitud = "-58.5", UbicacionDeReferencia = "Ubicacion" };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Assert.Equal("Todos los campos del apiario son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarApiario_ConLongitudVacia_LanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "Apiario", Latitud = "-34.5", Longitud = "", UbicacionDeReferencia = "Ubicacion" };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Assert.Equal("Todos los campos del apiario son obligatorios.", ex.Message);
        }

        [Fact]
        public void ValidarApiario_ConUbicacionVacia_LanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "Apiario", Latitud = "-34.5", Longitud = "-58.5", UbicacionDeReferencia = "" };

            // Act & Assert
            var ex = Assert.Throws<ApiarioException>(() => apiario.ValidarApiario());
            Assert.Equal("Todos los campos del apiario son obligatorios.", ex.Message);
        }

        [Fact]
        public void AgregarColmena_ConColmenaValida_AgregaSatisfactoriamente()
        {
            // Arrange
            var apiario = new Apiario("Apiario", "-34.5", "-58.5", "Ubicacion", 1);
            var colmena = new Colmena("Descripción", "C1", 1);

            // Act
            apiario.AgregarColmena(colmena);

            // Assert
            Assert.Single(apiario.Colmenas);
            Assert.Equal(colmena, apiario.Colmenas[0]);
        }

        [Fact]
        public void AgregarColmena_MultiplesColmenas_AgregaSatisfactoriamente()
        {
            // Arrange
            var apiario = new Apiario("Apiario", "-34.5", "-58.5", "Ubicacion", 1);
            var colmena1 = new Colmena("Descripción 1", "C1", 1);
            var colmena2 = new Colmena("Descripción 2", "C2", 1);
            var colmena3 = new Colmena("Descripción 3", "C3", 1);

            // Act
            apiario.AgregarColmena(colmena1);
            apiario.AgregarColmena(colmena2);
            apiario.AgregarColmena(colmena3);

            // Assert
            Assert.Equal(3, apiario.Colmenas.Count);
            Assert.Contains(colmena1, apiario.Colmenas);
            Assert.Contains(colmena2, apiario.Colmenas);
            Assert.Contains(colmena3, apiario.Colmenas);
        }

        [Fact]
        public void Apiario_ListaColmenasNoNula()
        {
            // Act
            var apiario = new Apiario();

            // Assert
            Assert.NotNull(apiario.Colmenas);
            Assert.Empty(apiario.Colmenas);
        }

        [Fact]
        public void AgregarColmena_ConColmenaNula_NoLanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario("Apiario", "-34.5", "-58.5", "Ubicacion", 1);

            // Act & Assert - Documenta que actualmente no valida null
            apiario.AgregarColmena(null);
            Assert.Single(apiario.Colmenas);
        }

        [Fact]
        public void ValidarApiario_ConEspaciosEnBlanco_NoLanzaExcepcion()
        {
            // Arrange
            var apiario = new Apiario { Nombre = "   ", Latitud = "-34.5", Longitud = "-58.5", UbicacionDeReferencia = "Ubicacion" };

            // Act & Assert - IsNullOrEmpty() considera espacios en blanco como válidos
            // Este test documenta el comportamiento actual
            apiario.ValidarApiario(); // No lanza excepción
        }
    }
}



