using LogicaDeNegocios;
using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.Registros;
using LogicaDeServicios.CasosDeUso.Notificaciones;
using LogicaDeServicios.CasosDeUso.Notificaciones.Canales;
using LogicaDeServicios.CasosDeUso.TomarMedicion;
using LogicaDeServicios.DTOs.Arduino;
using LogicaDeServicios.InterfacesCasosDeUso;
using Moq;
using Xunit;

namespace Tests.Integracion
{
    /// <summary>
    /// Tests de integración que prueban el flujo completo:
    /// AgregarMedicion → GenerarNotificacion → EnviadorNotificaciones → CanalSms → VonageServicioSms
    /// 
    /// ⚠️ IMPORTANTE: El test marcado con Skip envía SMS reales. 
    /// Solo ejecutar manualmente cuando se necesite verificar el flujo completo.
    /// </summary>
    public class NotificacionesSmsIntegrationTests
    {
        private const string NUMERO_PRUEBA = "+59891988714";

        // Mocks de repositorios
        private readonly Mock<IRepositorioCuadro> _mockRepoCuadros;
        private readonly Mock<IRepositorioColmena> _mockRepoColmenas;
        private readonly Mock<IRepositorioSensor> _mockRepoSensores;
        private readonly Mock<IRepositorioRegistroSensor> _mockRepoRegistrosSensores;
        private readonly Mock<IRepositorioRegistroMedicionColmena> _mockRepoRegistroMedicionColmena;
        private readonly Mock<IRepositorioNotificacion> _mockRepoNotificaciones;

        // Componentes reales del sistema de notificaciones
        private readonly GeneradorNotificaciones _generadorNotificaciones;
        private readonly Mock<IServicioSms> _mockServicioSms;

        public NotificacionesSmsIntegrationTests()
        {
            _mockRepoCuadros = new Mock<IRepositorioCuadro>();
            _mockRepoColmenas = new Mock<IRepositorioColmena>();
            _mockRepoSensores = new Mock<IRepositorioSensor>();
            _mockRepoRegistrosSensores = new Mock<IRepositorioRegistroSensor>();
            _mockRepoRegistroMedicionColmena = new Mock<IRepositorioRegistroMedicionColmena>();
            _mockRepoNotificaciones = new Mock<IRepositorioNotificacion>();
            _generadorNotificaciones = new GeneradorNotificaciones();
            _mockServicioSms = new Mock<IServicioSms>();

            // Inicializar configuración para los tests
            var configuraciones = new List<(string Nombre, string Valor)>
            {
                ("PesoMinimoColmena", "0"),
                ("PesoMaximo", "120"),
                ("TempHipotermia", "12"),
                ("TempCrias", "34")
            };
            Configuracion.Inicializar(configuraciones);
        }

        #region Tests con Mock (Sin envío real de SMS)

        [Fact]
        public void FlujoCompleto_MedicionPesoCero_GeneraYEnviaNotificacionSms()
        {
            // Arrange
            var usuario = CrearUsuarioDePrueba();
            var apiario = CrearApiarioDePrueba(usuario);
            var colmena = CrearColmenaDePrueba(apiario);
            var cuadro = CrearCuadroDePrueba(colmena);
            var sensor = CrearSensorDePrueba(colmena, cuadro);

            ConfigurarMocksRepositorios(sensor, colmena, cuadro);

            // Configurar mock del servicio SMS para capturar la llamada
            string mensajeEnviado = null;
            string numeroDestino = null;

            _mockServicioSms
                .Setup(s => s.EnviarAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((numero, mensaje) =>
                {
                    numeroDestino = numero;
                    mensajeEnviado = mensaje;
                })
                .Returns(Task.CompletedTask);

            // Crear el canal SMS con el mock
            var canalSms = new CanalSms(_mockServicioSms.Object);
            var canalEmailMock = new Mock<ICanalNotificacion>().Object;
            var canalWhatsAppMock = new Mock<ICanalNotificacion>().Object;
            var canalFrontendMock = new Mock<ICanalNotificacion>().Object;

            // Crear el enviador y suscribirlo al generador
            var enviadorNotificaciones = new EnviadorNotificaciones(canalSms, canalEmailMock, canalWhatsAppMock, canalFrontendMock);
            _generadorNotificaciones.SuscribirObservador(enviadorNotificaciones);

            // Crear el caso de uso
            var agregarMedicion = new AgregarMedicion(
                _mockRepoCuadros.Object,
                _mockRepoColmenas.Object,
                _mockRepoSensores.Object,
                _mockRepoRegistrosSensores.Object,
                _mockRepoRegistroMedicionColmena.Object,
                _mockRepoNotificaciones.Object,
                _generadorNotificaciones
            );

            // Record constructor: idSensor, tipoSensor, peso, tempExterna, tempInterna1, tempInterna2, tempInterna3
            var dto = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "peso",
                peso: 0,           // Peso cero - debe generar alerta
                tempExterna: 0,
                tempInterna1: 0,
                tempInterna2: 0,
                tempInterna3: 0
            );

            // Act
            agregarMedicion.Agregar(dto);

            // Assert - Esperar un momento para que el Task.Run complete
            Thread.Sleep(500);

            _mockServicioSms.Verify(s => s.EnviarAsync(
                It.Is<string>(n => n == NUMERO_PRUEBA),
                It.Is<string>(m => m.Contains("Peso"))),
                Times.Once);

            Assert.Equal(NUMERO_PRUEBA, numeroDestino);
            Assert.Contains("Peso", mensajeEnviado);
        }

        [Fact]
        public void FlujoCompleto_MedicionPesoMaximo_GeneraNotificacionCosecha()
        {
            // Arrange
            var usuario = CrearUsuarioDePrueba();
            var apiario = CrearApiarioDePrueba(usuario);
            var colmena = CrearColmenaDePrueba(apiario);
            var cuadro = CrearCuadroDePrueba(colmena);
            var sensor = CrearSensorDePrueba(colmena, cuadro);

            ConfigurarMocksRepositorios(sensor, colmena, cuadro);

            _mockServicioSms
                .Setup(s => s.EnviarAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var canalSms = new CanalSms(_mockServicioSms.Object);
            var enviadorNotificaciones = new EnviadorNotificaciones(
                canalSms,
                new Mock<ICanalNotificacion>().Object,
                new Mock<ICanalNotificacion>().Object,
                new Mock<ICanalNotificacion>().Object
            );
            _generadorNotificaciones.SuscribirObservador(enviadorNotificaciones);

            var agregarMedicion = new AgregarMedicion(
                _mockRepoCuadros.Object,
                _mockRepoColmenas.Object,
                _mockRepoSensores.Object,
                _mockRepoRegistrosSensores.Object,
                _mockRepoRegistroMedicionColmena.Object,
                _mockRepoNotificaciones.Object,
                _generadorNotificaciones
            );

            // Record constructor: idSensor, tipoSensor, peso, tempExterna, tempInterna1, tempInterna2, tempInterna3
            var dto = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "peso",
                peso: 125,          // Sobre el máximo (120) - debe generar alerta de cosecha
                tempExterna: 0,
                tempInterna1: 0,
                tempInterna2: 0,
                tempInterna3: 0
            );

            // Act
            agregarMedicion.Agregar(dto);

            // Assert
            Thread.Sleep(500);

            _mockServicioSms.Verify(s => s.EnviarAsync(
                NUMERO_PRUEBA,
                It.Is<string>(m => m.Contains("cosechar"))),
                Times.Once);
        }

        [Fact]
        public void FlujoCompleto_MedicionNormal_NoEnviaNotificacion()
        {
            // Arrange
            var usuario = CrearUsuarioDePrueba();
            var apiario = CrearApiarioDePrueba(usuario);
            var colmena = CrearColmenaDePrueba(apiario);
            var cuadro = CrearCuadroDePrueba(colmena);
            var sensor = CrearSensorDePrueba(colmena, cuadro);

            ConfigurarMocksRepositorios(sensor, colmena, cuadro);

            _mockServicioSms
                .Setup(s => s.EnviarAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var canalSms = new CanalSms(_mockServicioSms.Object);
            var enviadorNotificaciones = new EnviadorNotificaciones(
                canalSms,
                new Mock<ICanalNotificacion>().Object,
                new Mock<ICanalNotificacion>().Object,
                new Mock<ICanalNotificacion>().Object
            );
            _generadorNotificaciones.SuscribirObservador(enviadorNotificaciones);

            var agregarMedicion = new AgregarMedicion(
                _mockRepoCuadros.Object,
                _mockRepoColmenas.Object,
                _mockRepoSensores.Object,
                _mockRepoRegistrosSensores.Object,
                _mockRepoRegistroMedicionColmena.Object,
                _mockRepoNotificaciones.Object,
                _generadorNotificaciones
            );

            // Record constructor: idSensor, tipoSensor, peso, tempExterna, tempInterna1, tempInterna2, tempInterna3
            var dto = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "peso",
                peso: 25,          // Peso normal - NO debe generar alerta
                tempExterna: 0,
                tempInterna1: 0,
                tempInterna2: 0,
                tempInterna3: 0
            );

            // Act
            agregarMedicion.Agregar(dto);

            // Assert
            Thread.Sleep(500);

            _mockServicioSms.Verify(s => s.EnviarAsync(
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }

        #endregion

        #region Test de Integración Real (Envía SMS real via Vonage)

        // ESTE TEST ENVÍA UN SMS REAL A +59891988714
        // Solo ejecutar manualmente para verificar el flujo completo.
        // Requiere variables de entorno VONAGE_API_KEY, VONAGE_API_SECRET configuradas.
        [Fact(Skip = "Ejecutar manualmente - envía SMS real via Vonage")]
        //[Fact]
        public void FlujoCompletoReal_MedicionPesoCero_EnviaSmsRealViaVonage()
        {
            // seteamos las variables de entorno para Vonage (API Key, Secret, From Number)
            Environment.SetEnvironmentVariable("VONAGE_API_KEY", "3667ac03");
            Environment.SetEnvironmentVariable("VONAGE_API_SECRET", "AOBOax02F4jWGOob");
            Environment.SetEnvironmentVariable("VONAGE_FROM_NUMBER", "+59891988714");

            // Arrange
            var usuario = CrearUsuarioDePrueba();
            var apiario = CrearApiarioDePrueba(usuario);
            var colmena = CrearColmenaDePrueba(apiario);
            var cuadro = CrearCuadroDePrueba(colmena);
            var sensor = CrearSensorDePrueba(colmena, cuadro);

            // ⚠️ FIX: Ensure the full navigation chain is properly linked
            // sensor.Colmena -> Apiario -> Usuario
            sensor.Colmena = colmena;
            colmena.Apiario = apiario;
            apiario.Usuario = usuario;

            ConfigurarMocksRepositorios(sensor, colmena, cuadro);

            // Usar el servicio REAL de Vonage
            IServicioSms servicioSmsReal = new WebApi.Servicios.Sms.VonageServicioSms();

            var canalSms = new CanalSms(servicioSmsReal);
            var enviadorNotificaciones = new EnviadorNotificaciones(
                canalSms,
                new Mock<ICanalNotificacion>().Object,
                new Mock<ICanalNotificacion>().Object,
                new Mock<ICanalNotificacion>().Object
            );

            var generadorReal = new GeneradorNotificaciones();
            generadorReal.SuscribirObservador(enviadorNotificaciones);

            var agregarMedicion = new AgregarMedicion(
                _mockRepoCuadros.Object,
                _mockRepoColmenas.Object,
                _mockRepoSensores.Object,
                _mockRepoRegistrosSensores.Object,
                _mockRepoRegistroMedicionColmena.Object,
                _mockRepoNotificaciones.Object,
                generadorReal
            );

            var dto = new DataArduinoDto(
                idSensor: 1,
                tipoSensor: "peso",
                peso: 0,           // Peso cero - genera alerta
                tempExterna: 0,
                tempInterna1: 0,
                tempInterna2: 0,
                tempInterna3: 0
            );

            // Act
            agregarMedicion.Agregar(dto);

            // Assert - Wait for async Task.Run to complete
            Thread.Sleep(3000);

            Assert.True(true, "SMS enviado - verificar manualmente en el teléfono +59891988714");
        }

        #endregion

        #region Helper Methods

        private Usuario CrearUsuarioDePrueba()
        {
            return new Usuario("Clara Test", "clara@test.com", "password123", NUMERO_PRUEBA, "12", CanalPreferidoNotificacion.SMS)
            {
                Id = 1,
                MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS
            };
        }

        private Apiario CrearApiarioDePrueba(Usuario usuario)
        {
            return new Apiario
            {
                Id = 1,
                Nombre = "Apiario Test",
                Usuario = usuario,
                UsuarioId = usuario.Id
            };
        }

        private Colmena CrearColmenaDePrueba(Apiario apiario)
        {
            return new Colmena("Colmena de prueba", "C1", apiario.Id)
            {
                Id = 1,
                Apiario = apiario,
                Cuadros = new List<Cuadro>()
            };
        }

        private Cuadro CrearCuadroDePrueba(Colmena colmena)
        {
            return new Cuadro
            {
                Id = 1,
                ColmenaId = colmena.Id,
                Colmena = colmena
            };
        }

        private Sensor CrearSensorDePrueba(Colmena colmena, Cuadro cuadro)
        {
            return new Sensor("peso", 1, colmena, cuadro)
            {
                ColmenaId = colmena.Id,
                CuadroId = cuadro.Id
            };
        }

        private void ConfigurarMocksRepositorios(Sensor sensor, Colmena colmena, Cuadro cuadro)
        {
            _mockRepoSensores
                .Setup(r => r.ObtenerElementoPorId(It.IsAny<int>()))
                .Returns(sensor);

            _mockRepoColmenas
                .Setup(r => r.ObtenerElementoPorId(It.IsAny<int>()))
                .Returns(colmena);

            _mockRepoCuadros
                .Setup(r => r.ObtenerElementoPorId(It.IsAny<int>()))
                .Returns(cuadro);

            _mockRepoColmenas
                .Setup(r => r.AgregarMedicion(It.IsAny<MedicionColmena>(), It.IsAny<Colmena>()));

            _mockRepoRegistroMedicionColmena
                .Setup(r => r.Agregar(It.IsAny<RegistroMedicionColmena>()));

            _mockRepoNotificaciones
                .Setup(r => r.Agregar(It.IsAny<Notificacion>()));
        }

        #endregion
    }
}