using LogicaDeNegocios.Entidades;
using Xunit;
using System;

namespace LogicaDeNegocios.Tests.Entidades
{
    public class RegistroTests
    {
        [Fact]
        public void Constructor_ConParametros_CreaRegistroCorrectamente()
        {
            // Arrange
            var nombre = "Colmena";
            var tempInterna1 = 30.5f;
            var tempInterna2 = 31.2f;
            var tempInterna3 = 29.8f;
            var tempExterna = 25.0f;
            var peso = 45.3f;

            // Act
            var registro = new Registro(nombre, tempInterna1, tempInterna2, tempInterna3, tempExterna, peso);

            // Assert
            Assert.Equal(tempInterna1, registro.TempInterna1);
            Assert.Equal(tempInterna2, registro.TempInterna2);
            Assert.Equal(tempInterna3, registro.TempInterna3);
            Assert.Equal(tempExterna, registro.TempExterna);
            Assert.Equal(peso, registro.Peso);
            Assert.True(registro.Id >= 0);
            Assert.True((DateTime.Now - registro.FechaRegistro).TotalSeconds < 2);
        }

        [Fact]
        public void Constructor_SinParametros_CreaRegistroVacio()
        {
            // Act
            var registro = new Registro();

            // Assert
            Assert.NotNull(registro);
            Assert.Equal(0f, registro.TempInterna1);
            Assert.Equal(0f, registro.TempInterna2);
            Assert.Equal(0f, registro.TempInterna3);
            Assert.Equal(0f, registro.TempExterna);
            Assert.Equal(0f, registro.Peso);
        }

        [Theory]
        [InlineData("", 0f, 0f, 0f, 0f, 0f)]
        [InlineData("Colmena",15.5f, 16.0f, 15.8f, 10.0f, 30.0f)]
        [InlineData("Colmena",35.0f, 36.0f, 34.5f, 28.0f, 60.0f)]
        [InlineData("Colmena" ,- 5.0f, -4.0f, -6.0f, -10.0f, 20.0f)]
        public void Constructor_ConDiferentesValores_AsignaCorrectamente(
            string nombre, float temp1, float temp2, float temp3, float tempExt, float peso)
        {
            // Act
            var registro = new Registro(nombre, temp1, temp2, temp3, tempExt, peso);

            // Assert
            Assert.Equal(temp1, registro.TempInterna1);
            Assert.Equal(temp2, registro.TempInterna2);
            Assert.Equal(temp3, registro.TempInterna3);
            Assert.Equal(tempExt, registro.TempExterna);
            Assert.Equal(peso, registro.Peso);
        }

        [Fact]
        public void Registro_ConValoresNegativos_AceptaValoresNegativos()
        {
            // Arrange & Act
            var registro = new Registro("Colmena", -10.0f, -5.0f, -8.0f, -15.0f, -2.0f);

            // Assert
            Assert.Equal(-10.0f, registro.TempInterna1);
            Assert.Equal(-5.0f, registro.TempInterna2);
            Assert.Equal(-8.0f, registro.TempInterna3);
            Assert.Equal(-15.0f, registro.TempExterna);
            Assert.Equal(-2.0f, registro.Peso);
        }

        [Fact]
        public void Registro_ConValoresMuyAltos_AceptaValoresAltos()
        {
            // Arrange & Act
            var registro = new Registro("Colmena", 100.0f, 105.0f, 102.0f, 95.0f, 200.0f);

            // Assert
            Assert.Equal(100.0f, registro.TempInterna1);
            Assert.Equal(105.0f, registro.TempInterna2);
            Assert.Equal(102.0f, registro.TempInterna3);
            Assert.Equal(95.0f, registro.TempExterna);
            Assert.Equal(200.0f, registro.Peso);
        }

        [Fact]
        public void Registro_ConValoresDecimales_MantienePrecision()
        {
            // Arrange & Act
            var registro = new Registro("Colmena", 30.123f, 31.456f, 29.789f, 25.555f, 45.999f);

            // Assert
            Assert.Equal(30.123f, registro.TempInterna1, 3);
            Assert.Equal(31.456f, registro.TempInterna2, 3);
            Assert.Equal(29.789f, registro.TempInterna3, 3);
            Assert.Equal(25.555f, registro.TempExterna, 3);
            Assert.Equal(45.999f, registro.Peso, 3);
        }

        [Fact]
        public void Registro_PropiedadesSonModificables()
        {
            // Arrange
            var registro = new Registro("Colmena", 30.0f, 30.0f, 30.0f, 25.0f, 45.0f);

            // Act
            registro.TempInterna1 = 32.0f;
            registro.Peso = 50.0f;

            // Assert
            Assert.Equal(32.0f, registro.TempInterna1);
            Assert.Equal(50.0f, registro.Peso);
        }
    }
}