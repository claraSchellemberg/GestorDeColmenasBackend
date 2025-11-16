using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using Xunit;
using System;

namespace LogicaDeNegocios.Tests.Entidades
{
    public class NotificacionTests
    {
        [Fact]
        public void Constructor_ConDatosValidos_CreaNotificacionCorrectamente()
        {
            // Arrange
            var mensaje = "Temperatura crítica detectada";
            var registro = new Registro("Colmena", 35.0f, 36.0f, 34.5f, 25.0f, 50.0f);

            // Act
            var notificacion = new Notificacion(mensaje, registro);

            // Assert
            Assert.Equal(mensaje, notificacion.Mensaje);
            Assert.Equal(registro, notificacion.RegistroAsociado);
            Assert.True(notificacion.Id >= 0);
            Assert.True((DateTime.Now - notificacion.FechaNotificacion).TotalSeconds < 2);
        }

        [Fact]
        public void ValidarNotificacion_ConDatosValidos_NoLanzaExcepcion()
        {
            // Arrange
            var registro = new Registro("Colmena", 30.0f, 31.0f, 29.0f, 25.0f, 45.0f);
            var notificacion = new Notificacion("Mensaje válido", registro);

            // Act & Assert
            var exception = Record.Exception(() => notificacion.ValidarNotificacion());
            Assert.Null(exception);
        }

        [Fact]
        public void ValidarNotificacion_ConMensajeVacio_LanzaNotificacionException()
        {
            // Arrange
            var registro = new Registro("Colmena", 30.0f, 31.0f, 29.0f, 25.0f, 45.0f);
            var notificacion = new Notificacion("", registro);

            // Act & Assert
            var exception = Assert.Throws<NotificacionException>(() => notificacion.ValidarNotificacion());
            Assert.Equal("El mensaje y el registro asociado son obligatorios.", exception.Message);
        }

        [Fact]
        public void ValidarNotificacion_ConMensajeNull_LanzaNotificacionException()
        {
            // Arrange
            var registro = new Registro("Colmena", 30.0f, 31.0f, 29.0f, 25.0f, 45.0f);
            var notificacion = new Notificacion(null, registro);

            // Act & Assert
            var exception = Assert.Throws<NotificacionException>(() => notificacion.ValidarNotificacion());
            Assert.Equal("El mensaje y el registro asociado son obligatorios.", exception.Message);
        }

        [Fact]
        public void ValidarNotificacion_ConRegistroNull_LanzaNotificacionException()
        {
            // Arrange
            var notificacion = new Notificacion("Mensaje válido", null);

            // Act & Assert
            var exception = Assert.Throws<NotificacionException>(() => notificacion.ValidarNotificacion());
            Assert.Equal("El mensaje y el registro asociado son obligatorios.", exception.Message);
        }

        [Fact]
        public void ValidarNotificacion_ConMensajeYRegistroNull_LanzaNotificacionException()
        {
            // Arrange
            var notificacion = new Notificacion(null, null);

            // Act & Assert
            var exception = Assert.Throws<NotificacionException>(() => notificacion.ValidarNotificacion());
            Assert.Equal("El mensaje y el registro asociado son obligatorios.", exception.Message);
        }

        [Fact]
        public void Constructor_ConMensajeLargo_AsignaMensajeCorrectamente()
        {
            // Arrange
            var mensajeLargo = "Este es un mensaje muy largo que podría contener mucha información sobre el estado de la colmena y sus métricas";
            var registro = new Registro("Colmena", 30.0f, 31.0f, 29.0f, 25.0f, 45.0f);

            // Act
            var notificacion = new Notificacion(mensajeLargo, registro);

            // Assert
            Assert.Equal(mensajeLargo, notificacion.Mensaje);
        }
    }
}