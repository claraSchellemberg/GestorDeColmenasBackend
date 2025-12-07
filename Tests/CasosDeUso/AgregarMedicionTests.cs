using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.CasosDeUso.TomarMedicion;
using LogicaDeServicios.DTOs.Arduino;
using Moq;
using Xunit;

namespace Tests.CasosDeUso
{
    public class AgregarMedicionTests
    {
        private readonly Mock<IRepositorioCuadro> _mockRepoCuadros;
        private readonly Mock<IRepositorioColmena> _mockRepoColmenas;
        private readonly Mock<IRepositorioSensor> _mockRepoSensores;
        private readonly AgregarMedicion _agregarMedicion;

        public AgregarMedicionTests()
        {
            _mockRepoCuadros = new Mock<IRepositorioCuadro>();
            _mockRepoColmenas = new Mock<IRepositorioColmena>();
            _mockRepoSensores = new Mock<IRepositorioSensor>();
            
            _agregarMedicion = new AgregarMedicion(
                _mockRepoCuadros.Object,
                _mockRepoColmenas.Object,
                _mockRepoSensores.Object);
        }

        #region Test Cases - Happy Path

        [Fact]
        public void Agregar_ConDataValida_DebeCrearMedicionColmenaYSensorPorCuadro()
        {
            // Arrange
            var sensor = new Sensor
            {
                SensorId = 1,
                TipoSensor = "Temperatura",
                ColmenaId = 1,
                CuadroId = 1
            };

            var colmena = new Colmena("Desc", "Colmena A1") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 45.5f,
                tempExterna: 25.3f,
                temp1: 34.5f,
                temp2: 34.6f,
                temp3: 34.4f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1))
                .Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1))
                .Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1))
                .Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoColmenas.Verify(
                r => r.AgregarMedicion(
                    It.Is<MedicionColmena>(m => 
                        m.Peso == 45.5f && 
                        m.TempExterna == 25.3f),
                    It.IsAny<Colmena>()),
                Times.Once);

            _mockRepoCuadros.Verify(
                r => r.AgregarMedicionDeCuadro(
                    It.Is<SensorPorCuadro>(m => 
                        m.TempInterna1 == 34.5f &&
                        m.TempInterna2 == 34.6f &&
                        m.TempInterna3 == 34.4f),
                    It.IsAny<Cuadro>()),
                Times.Once);
        }

        #endregion

        #region Test Cases - Edge Cases

        [Fact]
        public void Agregar_ConTemperaturaExtremaAlta_DebeGuardarCorrectamente()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 1 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 50.0f,
                tempExterna: 50.0f,  // Temperatura muy alta
                temp1: 45.0f,
                temp2: 45.5f,
                temp3: 45.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoColmenas.Verify(
                r => r.AgregarMedicion(
                    It.Is<MedicionColmena>(m => m.TempExterna == 50.0f),
                    It.IsAny<Colmena>()),
                Times.Once);
        }

        [Fact]
        public void Agregar_ConTemperaturaBaja_DebeGuardarCorrectamente()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 1 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 35.0f,
                tempExterna: 5.0f,  // Temperatura muy baja
                temp1: 15.0f,
                temp2: 14.5f,
                temp3: 15.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(It.IsAny<MedicionColmena>(), It.IsAny<Colmena>()), Times.Once);
        }

        [Fact]
        public void Agregar_ConPesoMaximo_DebeGuardarCorrectamente()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 1 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Peso",
                peso: 500.0f,  // Peso muy alto
                tempExterna: 25.0f,
                temp1: 35.0f,
                temp2: 35.5f,
                temp3: 35.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoColmenas.Verify(
                r => r.AgregarMedicion(
                    It.Is<MedicionColmena>(m => m.Peso == 500.0f),
                    It.IsAny<Colmena>()),
                Times.Once);
        }

        [Fact]
        public void Agregar_ConPesoMuyBajo_DebeGuardarCorrectamente()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 1 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Peso",
                peso: 0.1f,  // Peso muy bajo
                tempExterna: 25.0f,
                temp1: 35.0f,
                temp2: 35.5f,
                temp3: 35.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoColmenas.Verify(r => r.AgregarMedicion(It.IsAny<MedicionColmena>(), It.IsAny<Colmena>()), Times.Once);
        }

        [Fact]
        public void Agregar_ConTemperaturasInternasDiferentes_DebeGuardarTodasCorrectamente()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 1 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 45.0f,
                tempExterna: 25.0f,
                temp1: 30.0f,
                temp2: 40.0f,
                temp3: 35.0f  // Temperaturas muy diferentes
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(It.IsAny<int>())).Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoCuadros.Verify(
                r => r.AgregarMedicionDeCuadro(
                    It.Is<SensorPorCuadro>(m =>
                        m.TempInterna1 == 30.0f &&
                        m.TempInterna2 == 40.0f &&
                        m.TempInterna3 == 35.0f),
                    It.IsAny<Cuadro>()),
                Times.Once);
        }

        #endregion

        #region Test Cases - Error Handling

        [Fact]
        public void Agregar_ConSensorNoExistente_DebeCapturarExcepcion()
        {
            // Arrange
            var dataArduino = new DataArduinoDto(
                idSensor: 999,
                tipoSensor: "Temperatura",
                peso: 45.0f,
                tempExterna: 25.0f,
                temp1: 35.0f,
                temp2: 35.5f,
                temp3: 35.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(999))
                .Throws(new Exception("Sensor no encontrado"));

            // Act & Assert
            Assert.Throws<Exception>(() => _agregarMedicion.Agregar(dataArduino));
        }

        [Fact]
        public void Agregar_ConColmenaNoExistente_DebeCapturarExcepcion()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 999, CuadroId = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 45.0f,
                tempExterna: 25.0f,
                temp1: 35.0f,
                temp2: 35.5f,
                temp3: 35.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(999))
                .Throws(new Exception("Colmena no encontrada"));

            // Act & Assert
            Assert.Throws<Exception>(() => _agregarMedicion.Agregar(dataArduino));
        }

        [Fact]
        public void Agregar_ConCuadroNoExistente_DebeCapturarExcepcion()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 999 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 45.0f,
                tempExterna: 25.0f,
                temp1: 35.0f,
                temp2: 35.5f,
                temp3: 35.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(999))
                .Throws(new Exception("Cuadro no encontrado"));

            // Act & Assert
            Assert.Throws<Exception>(() => _agregarMedicion.Agregar(dataArduino));
        }

        #endregion

        #region Test Cases - Verification of Repository Calls

        [Fact]
        public void Agregar_DebeObtenerSensorDelRepositorio()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 1 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 45.0f,
                tempExterna: 25.0f,
                temp1: 35.0f,
                temp2: 35.5f,
                temp3: 35.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoSensores.Verify(r => r.ObtenerElementoPorId(1), Times.Once);
        }

        [Fact]
        public void Agregar_DebeObtenerColmenaDelRepositorio()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 1 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 45.0f,
                tempExterna: 25.0f,
                temp1: 35.0f,
                temp2: 35.5f,
                temp3: 35.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoColmenas.Verify(r => r.ObtenerElementoPorId(1), Times.Once);
        }

        [Fact]
        public void Agregar_DebeObtenerCuadroDelRepositorio()
        {
            // Arrange
            var sensor = new Sensor { SensorId = 1, ColmenaId = 1, CuadroId = 1 };
            var colmena = new Colmena("Desc", "Col A") { Id = 1 };
            var cuadro = new Cuadro { Id = 1 };

            var dataArduino = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "Temperatura",
                peso: 45.0f,
                tempExterna: 25.0f,
                temp1: 35.0f,
                temp2: 35.5f,
                temp3: 35.2f
            );

            _mockRepoSensores.Setup(r => r.ObtenerElementoPorId(1)).Returns(sensor);
            _mockRepoColmenas.Setup(r => r.ObtenerElementoPorId(1)).Returns(colmena);
            _mockRepoCuadros.Setup(r => r.ObtenerElementoPorId(1)).Returns(cuadro);

            // Act
            _agregarMedicion.Agregar(dataArduino);

            // Assert
            _mockRepoCuadros.Verify(r => r.ObtenerElementoPorId(1), Times.Once);
        }

        #endregion
    }
}