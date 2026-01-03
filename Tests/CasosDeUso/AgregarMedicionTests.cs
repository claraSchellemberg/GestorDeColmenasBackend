using LogicaDeNegocios;
using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.CasosDeUso.TomarMedicion;
using LogicaDeServicios.DTOs.Arduino;
using LogicaDeServicios.InterfacesCasosDeUso;
using Moq;
using Xunit;

namespace LogicaDeServicios.Tests.CasosDeUso.TomarMedicion
{
    public class AgregarMedicionTests
    {
        private readonly Mock<IRepositorioCuadro> _mockRepoCuadros;
        private readonly Mock<IRepositorioColmena> _mockRepoColmenas;
        private readonly Mock<IRepositorioSensor> _mockRepoSensores;
        private readonly Mock<IRepositorioRegistroSensor> _mockRepoRegistrosSensores;
        private readonly Mock<IRepositorioRegistroMedicionColmena> _mockRepoRegistroMedicionColmena;
        private readonly Mock<IRepositorioNotificacion> _mockRepoNotificaciones;
        private readonly Mock<IGeneradorNotificaciones> _mockGeneradorNotificaciones;
        private readonly AgregarMedicion _agregarMedicion;

        public AgregarMedicionTests()
        {
            _mockRepoCuadros = new Mock<IRepositorioCuadro>();
            _mockRepoColmenas = new Mock<IRepositorioColmena>();
            _mockRepoSensores = new Mock<IRepositorioSensor>();
            _mockRepoRegistrosSensores = new Mock<IRepositorioRegistroSensor>();
            _mockRepoRegistroMedicionColmena = new Mock<IRepositorioRegistroMedicionColmena>();
            _mockRepoNotificaciones = new Mock<IRepositorioNotificacion>();
            _mockGeneradorNotificaciones = new Mock<IGeneradorNotificaciones>();

            _agregarMedicion = new AgregarMedicion(
                _mockRepoCuadros.Object,
                _mockRepoColmenas.Object,
                _mockRepoSensores.Object,
                _mockRepoRegistrosSensores.Object,
                _mockRepoRegistroMedicionColmena.Object,
                _mockRepoNotificaciones.Object,
                _mockGeneradorNotificaciones.Object
            );

            InicializarConfiguracion();
        }

        private void InicializarConfiguracion()
        {
            var configs = new List<(string Nombre, string Valor)>
            {
                ("PesoMinimoColmena", "0"),
                ("PesoMaximo", "120"),
                ("TempHipotermia", "12"),
                ("TempCrias", "34")
            };
            Configuracion.Inicializar(configs);
        }

        private Sensor CrearSensorConColmena(int sensorId = 1, int colmenaId = 1)
        {
            var colmena = new Colmena
            {
                Id = colmenaId,
                Nombre = "Colmena Test",
                Cuadros = new List<Cuadro>()
            };

            return new Sensor
            {
                SensorId = sensorId,
                ColmenaId = colmenaId,
                Colmena = colmena
            };
        }

        #region Tests para Medición de Colmena (peso y tempExterna)

        [Fact]
        public void Agregar_ConPesoPositivo_DebeCrearMedicionColmena()
        {
            // Arrange
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "peso", 50, 25, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(
                It.Is<MedicionColmena>(m => m.Peso == 50 && m.TempExterna == 25),
                It.IsAny<Colmena>()), Times.Once);
            _mockRepoRegistroMedicionColmena.Verify(r => r.Agregar(
                It.IsAny<RegistroMedicionColmena>()), Times.Once);
        }

        [Fact]
        public void Agregar_ConPesoNegativo_NoDebeCrearMedicionColmena()
        {
            // Arrange
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "peso", -10, -5, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(
                It.IsAny<MedicionColmena>(),
                It.IsAny<Colmena>()), Times.Never);
        }

        [Fact]
        public void Agregar_ConPesoEnLimiteMaximo_DebeCrearMedicionYGenerarNotificacion()
        {
            // Arrange - PesoMaximo is 120
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "peso", 120, 25, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(
                It.IsAny<MedicionColmena>(),
                It.IsAny<Colmena>()), Times.Once);
            _mockRepoRegistroMedicionColmena.Verify(r => r.Agregar(
                It.Is<RegistroMedicionColmena>(rm => rm.ValorEstaEnRangoBorde == true)), Times.Once);
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.Is<Notificacion>(n => n.Mensaje.Contains("umbral máximo"))), Times.Once);
            _mockGeneradorNotificaciones.Verify(g => g.NotificarObservadores(
                It.IsAny<Notificacion>()), Times.Once);
        }

        [Fact]
        public void Agregar_ConPesoSobreMaximo_DebeGenerarNotificacionDeCosecha()
        {
            // Arrange - PesoMaximo is 120
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "peso", 150, 25, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.Is<Notificacion>(n => n.Mensaje.Contains("cosechar"))), Times.Once);
        }

        [Fact]
        public void Agregar_ConPesoEnLimiteMinimo_DebeGenerarNotificacion()
        {
            // Arrange - PesoMinimoColmena is 0, so peso <= 0 triggers alert
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "peso", 0.1f, 25, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert - peso 0.1 > 0 (PesoMinimo), so no notification for minimum
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(
                It.IsAny<MedicionColmena>(),
                It.IsAny<Colmena>()), Times.Once);
        }

        [Fact]
        public void Agregar_ConPesoNormal_NoDebeGenerarNotificacion()
        {
            // Arrange - Peso between min and max
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "peso", 50, 25, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.IsAny<Notificacion>()), Times.Never);
        }

        [Fact]
        public void Agregar_SoloConTempExternaPositiva_DebeCrearMedicionColmena()
        {
            // Arrange
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "temperatura", 0, 30, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(
                It.Is<MedicionColmena>(m => m.TempExterna == 30),
                It.IsAny<Colmena>()), Times.Once);
        }

        [Fact]
        public void Agregar_ConPesoCero_YTipoSensorPeso_DebeCrearMedicionYGenerarNotificacion()
        {
            // Arrange - peso=0 with tipoSensor="peso" indicates a fallen hive or sensor issue
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "peso", 0, 0, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert - Should create measurement AND generate notification for zero weight
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(
                It.Is<MedicionColmena>(m => m.Peso == 0),
                It.IsAny<Colmena>()), Times.Once);
            _mockRepoRegistroMedicionColmena.Verify(r => r.Agregar(
                It.Is<RegistroMedicionColmena>(rm => rm.ValorEstaEnRangoBorde == true)), Times.Once);
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.Is<Notificacion>(n => n.Mensaje.Contains("cero"))), Times.Once);
        }

        [Fact]
        public void Agregar_ConPesoCeroYTempExternaCero_YTipoSensorTemperatura_NoDebeCrearMedicionColmena()
        {
            // Arrange - All zeros with tipoSensor != "peso" should not create any measurement
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "temperatura", 0, 0, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(
                It.IsAny<MedicionColmena>(),
                It.IsAny<Colmena>()), Times.Never);
        }

        [Fact]
        public void Agregar_ConTemperaturasInternas_DebeCrearMedicionDeCuadro()
        {
            // Arrange
            var cuadro = new Cuadro { Id = 1 };
            var colmena = new Colmena
            {
                Id = 1,
                Nombre = "Colmena Test",
                Cuadros = new List<Cuadro> { cuadro }
            };
            var sensor = new Sensor
            {
                SensorId = 1,
                ColmenaId = 1,
                CuadroId = 1,
                Colmena = colmena
            };
            // Use tipoSensor="temperatura" and peso=0, tempExterna=0 so it goes to cuadro logic
            var dto = new DataArduinoDto(1, "temperatura", 0, 0, 35, 36, 37);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoCuadros.Verify(r => r.AgregarMedicionDeCuadro(
                It.Is<SensorPorCuadro>(m => m.TempInterna1 == 35 && m.TempInterna2 == 36 && m.TempInterna3 == 37),
                It.IsAny<Cuadro>()), Times.Once);
            _mockRepoRegistrosSensores.Verify(r => r.Agregar(
                It.IsAny<RegistroSensor>()), Times.Once);
        }

        [Fact]
        public void Agregar_ConTemperaturaEnRangoBordeHipotermia_DebeMarcarRegistroYGenerarNotificacion()
        {
            // Arrange - TempHipotermia is 12
            var cuadro1 = new Cuadro { Id = 1 };
            var cuadro2 = new Cuadro { Id = 2 };
            var colmena = new Colmena
            {
                Id = 1,
                Nombre = "Colmena Test",
                Cuadros = new List<Cuadro> { cuadro1, cuadro2 }
            };
            var sensor = new Sensor
            {
                SensorId = 1,
                ColmenaId = 1,
                CuadroId = 1,
                Colmena = colmena
            };
            // tipoSensor="temperatura", peso=0, tempExterna=0 ensures we go to cuadro logic
            var dto = new DataArduinoDto(1, "temperatura", 0, 0, 12, 35, 35);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro1);
            
            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(1))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = true });
            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(2))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = false });

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoRegistrosSensores.Verify(r => r.Agregar(
                It.Is<RegistroSensor>(rs => rs.ValorEstaEnRangoBorde == true)), Times.Once);
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.Is<Notificacion>(n => n.Mensaje.Contains("hipotermia"))), Times.Once);
        }

        [Fact]
        public void Agregar_ConTemperaturaBajoHipotermia_DebeMarcarRegistroYGenerarNotificacion()
        {
            // Arrange - TempHipotermia is 12
            var cuadro1 = new Cuadro { Id = 1 };
            var cuadro2 = new Cuadro { Id = 2 };
            var colmena = new Colmena
            {
                Id = 1,
                Nombre = "Colmena Test",
                Cuadros = new List<Cuadro> { cuadro1, cuadro2 }
            };
            var sensor = new Sensor
            {
                SensorId = 1,
                ColmenaId = 1,
                CuadroId = 1,
                Colmena = colmena
            };
            var dto = new DataArduinoDto(1, "temperatura", 0, 0, 5, 35, 35);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro1);
            
            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(1))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = true });
            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(2))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = false });

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            _mockRepoRegistrosSensores.Verify(r => r.Agregar(
                It.Is<RegistroSensor>(rs => rs.ValorEstaEnRangoBorde == true)), Times.Once);
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.Is<Notificacion>(n => n.Mensaje.Contains("hipotermia"))), Times.Once);
        }

        [Fact]
        public void Agregar_ConMultiplesCondicionesDeAlerta_DebeGenerarMultiplesNotificaciones()
        {
            // Arrange - Temperature that triggers both hipotermia AND crias alerts
            var cuadro1 = new Cuadro { Id = 1 };
            var cuadro2 = new Cuadro { Id = 2 };
            var colmena = new Colmena
            {
                Id = 1,
                Nombre = "Colmena Test",
                Cuadros = new List<Cuadro> { cuadro1, cuadro2 }
            };
            var sensor = new Sensor
            {
                SensorId = 1,
                ColmenaId = 1,
                CuadroId = 1,
                Colmena = colmena
            };
            // temp1=5 (hipotermia), temp2=30 (below crias threshold of 34), temp3=35 (normal)
            var dto = new DataArduinoDto(1, "temperatura", 0, 0, 5, 30, 35);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro1);
            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(1))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = true });
            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(2))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = false });

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert - Should generate 2 notifications (hipotermia + crias)
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.Is<Notificacion>(n => n.Mensaje.Contains("hipotermia"))), Times.Once);
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.IsAny<Notificacion>()), Times.Once);
        }

        [Fact]
        public void Agregar_TodosCuadrosEnRangoBorde_DebeGenerarNotificacionDeColmenaYCambiarCondicion()
        {
            // Arrange
            var cuadro1 = new Cuadro { Id = 1 };
            var cuadro2 = new Cuadro { Id = 2 };
            var colmena = new Colmena
            {
                Id = 1,
                Nombre = "Colmena Test",
                Condicion = CondicionColmena.OPTIMO,
                Cuadros = new List<Cuadro> { cuadro1, cuadro2 }
            };
            var sensor = new Sensor
            {
                SensorId = 1,
                ColmenaId = 1,
                CuadroId = 1,
                Colmena = colmena
            };
            var dto = new DataArduinoDto(1, "temperatura", 0, 0, 10, 10, 10);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro1);

            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(1))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = true });
            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(2))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = true });

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert - Should generate notifications for individual sensor AND for colmena state
            // Individual: hipotermia alert (temp 10 <= 12)
            // Colmena-level: ALERTA for all cuadros in danger
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.IsAny<Notificacion>()), Times.AtLeast(2));
            _mockGeneradorNotificaciones.Verify(g => g.NotificarObservadores(
                It.IsAny<Notificacion>()), Times.AtLeastOnce);
            _mockRepoColmenas.Verify(r => r.Actualizar(
                It.Is<Colmena>(c => c.Condicion == CondicionColmena.EN_PELIGRO)), Times.Once);
        }

        [Fact]
        public void Agregar_NoTodosCuadrosEnRangoBorde_NoDebeGenerarNotificacionDeColmena()
        {
            // Arrange
            var cuadro1 = new Cuadro { Id = 1 };
            var cuadro2 = new Cuadro { Id = 2 };
            var colmena = new Colmena
            {
                Id = 1,
                Nombre = "Colmena Test",
                Condicion = CondicionColmena.OPTIMO,
                Cuadros = new List<Cuadro> { cuadro1, cuadro2 }
            };
            var sensor = new Sensor
            {
                SensorId = 1,
                ColmenaId = 1,
                CuadroId = 1,
                Colmena = colmena
            };
            var dto = new DataArduinoDto(1, "temperatura", 0, 0, 10, 10, 10);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro1);

            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(1))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = true });
            _mockRepoRegistrosSensores.Setup(r => r.ObtenerUltimoPorCuadro(2))
                .Returns(new RegistroSensor { ValorEstaEnRangoBorde = false });

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert - Individual notification yes, but no colmena-level notification
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.Is<Notificacion>(n => n.Mensaje.Contains("ALERTA") && n.Mensaje.Contains("Colmena Test"))), Times.Never);
            _mockRepoColmenas.Verify(r => r.Actualizar(
                It.IsAny<Colmena>()), Times.Never);
        }

        [Fact]
        public void Agregar_ColmenaSinCuadros_NoDebeGenerarNotificacionDeColmena()
        {
            // Arrange
            var colmena = new Colmena
            {
                Id = 1,
                Nombre = "Colmena Test",
                Cuadros = new List<Cuadro>()
            };
            var sensor = new Sensor
            {
                SensorId = 1,
                ColmenaId = 1,
                CuadroId = 1,
                Colmena = colmena
            };
            var cuadro = new Cuadro { Id = 1 };
            var dto = new DataArduinoDto(1, "temperatura", 0, 0, 10, 10, 10);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert - Individual notification for sensor, but no colmena-level notification
            _mockRepoNotificaciones.Verify(r => r.Agregar(
                It.Is<Notificacion>(n => n.Mensaje.Contains("ALERTA") && n.Mensaje.Contains("Colmena Test"))), Times.Never);
        }

        #endregion

        #region Tests para Sensor Inválido

        [Fact]
        public void Agregar_ConSensorInexistente_DebeLanzarExcepcion()
        {
            // Arrange
            var dto = new DataArduinoDto(999, "peso", 50, 0, 0, 0, 0);
            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(999))
                .Throws(new Exception("Sensor no encontrado"));

            // Act & Assert
            Assert.Throws<Exception>(() => _agregarMedicion.Agregar(dto));
        }

        #endregion

        #region Tests para Retorno de Datos

        [Fact]
        public void Agregar_DebeRetornarMismoDtoRecibido()
        {
            // Arrange
            var sensor = CrearSensorConColmena();
            var dto = new DataArduinoDto(1, "peso", 50, 25, 0, 0, 0);

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(sensor.ColmenaId)).Returns(sensor.Colmena);

            // Act
            var result = _agregarMedicion.Agregar(dto);

            // Assert
            Assert.Equal(dto, result);
            Assert.Equal(1, result.idSensor);
            Assert.Equal(50, result.peso);
            Assert.Equal(25, result.tempExterna);
        }

        #endregion
    }
}