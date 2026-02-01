using LogicaDeNegocios;
using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
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
    /// AgregarMedicion → GenerarNotificacion → EnviadorNotificaciones → CanalEmail → ServicioEmail (SendGrid)
    /// 
    /// ⚠️ IMPORTANTE: El test marcado con Skip envía emails reales. 
    /// Solo ejecutar manualmente cuando se necesite verificar el flujo completo.
    /// </summary>
    public class NotificacionesEmailIntegrationTests
    {
        private const string EMAIL_PRUEBA = "clara@test.com";

        // Mocks de repositorios
        private readonly Mock<IRepositorioCuadro> _mockRepoCuadros;
        private readonly Mock<IRepositorioColmena> _mockRepoColmenas;
        private readonly Mock<IRepositorioSensor> _mockRepoSensores;
        private readonly Mock<IRepositorioRegistroSensor> _mockRepoRegistrosSensores;
        private readonly Mock<IRepositorioRegistroMedicionColmena> _mockRepoRegistroMedicionColmena;
        private readonly Mock<IRepositorioNotificacion> _mockRepoNotificaciones;

        // Componentes reales del sistema de notificaciones
        private readonly GeneradorNotificaciones _generadorNotificaciones;
        private readonly Mock<IServicioEmail> _mockServicioEmail;

        public NotificacionesEmailIntegrationTests()
        {
            _mockRepoCuadros = new Mock<IRepositorioCuadro>();
            _mockRepoColmenas = new Mock<IRepositorioColmena>();
            _mockRepoSensores = new Mock<IRepositorioSensor>();
            _mockRepoRegistrosSensores = new Mock<IRepositorioRegistroSensor>();
            _mockRepoRegistroMedicionColmena = new Mock<IRepositorioRegistroMedicionColmena>();
            _mockRepoNotificaciones = new Mock<IRepositorioNotificacion>();
            _generadorNotificaciones = new GeneradorNotificaciones();
            _mockServicioEmail = new Mock<IServicioEmail>();

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

        #region Tests con Mock (Sin envío real de Email)

        [Fact]
        public void FlujoCompleto_MedicionPesoCero_GeneraYEnviaNotificacionEmail()
        {
            // Arrange
            var usuario = CrearUsuarioDePrueba();
            var apiario = CrearApiarioDePrueba(usuario);
            var colmena = CrearColmenaDePrueba(apiario);
            var cuadro = CrearCuadroDePrueba(colmena);
            var sensor = CrearSensorDePrueba(colmena, cuadro);

            ConfigurarMocksRepositorios(sensor, colmena, cuadro);

            // Configurar mock del servicio Email para capturar la llamada
            Notificacion notificacionEnviada = null;
            Usuario usuarioDestino = null;

            _mockServicioEmail
                .Setup(s => s.EnviarAsync(It.IsAny<Notificacion>(), It.IsAny<Usuario>()))
                .Callback<Notificacion, Usuario>((notificacion, user) =>
                {
                    notificacionEnviada = notificacion;
                    usuarioDestino = user;
                })
                .Returns(Task.CompletedTask);

            // Crear el canal Email con el mock
            var canalEmail = new CanalEmail(_mockServicioEmail.Object);
            var canalSmsMock = new Mock<ICanalNotificacion>().Object;
            var canalWhatsAppMock = new Mock<ICanalNotificacion>().Object;
            var canalFrontendMock = new Mock<ICanalNotificacion>().Object;

            // Crear el enviador y suscribirlo al generador
            var enviadorNotificaciones = new EnviadorNotificaciones(canalSmsMock, canalEmail, canalWhatsAppMock, canalFrontendMock);
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

            _mockServicioEmail.Verify(s => s.EnviarAsync(
                It.Is<Notificacion>(n => n.Mensaje.Contains("Peso")),
                It.Is<Usuario>(u => u.Email == EMAIL_PRUEBA)),
                Times.Once);

            Assert.Equal(EMAIL_PRUEBA, usuarioDestino?.Email);
            Assert.Contains("Peso", notificacionEnviada?.Mensaje);
        }

        [Fact]
        public void FlujoCompleto_MedicionPesoMaximo_GeneraNotificacionCosechaEmail()
        {
            // Arrange
            var usuario = CrearUsuarioDePrueba();
            var apiario = CrearApiarioDePrueba(usuario);
            var colmena = CrearColmenaDePrueba(apiario);
            var cuadro = CrearCuadroDePrueba(colmena);
            var sensor = CrearSensorDePrueba(colmena, cuadro);

            ConfigurarMocksRepositorios(sensor, colmena, cuadro);

            _mockServicioEmail
                .Setup(s => s.EnviarAsync(It.IsAny<Notificacion>(), It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);

            var canalEmail = new CanalEmail(_mockServicioEmail.Object);
            var enviadorNotificaciones = new EnviadorNotificaciones(
                new Mock<ICanalNotificacion>().Object,
                canalEmail,
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

            _mockServicioEmail.Verify(s => s.EnviarAsync(
                It.Is<Notificacion>(n => n.Mensaje.Contains("cosechar")),
                It.Is<Usuario>(u => u.Email == EMAIL_PRUEBA)),
                Times.Once);
        }

        [Fact]
        public void FlujoCompleto_MedicionNormal_NoEnviaNotificacionEmail()
        {
            // Arrange
            var usuario = CrearUsuarioDePrueba();
            var apiario = CrearApiarioDePrueba(usuario);
            var colmena = CrearColmenaDePrueba(apiario);
            var cuadro = CrearCuadroDePrueba(colmena);
            var sensor = CrearSensorDePrueba(colmena, cuadro);

            ConfigurarMocksRepositorios(sensor, colmena, cuadro);

            _mockServicioEmail
                .Setup(s => s.EnviarAsync(It.IsAny<Notificacion>(), It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);

            var canalEmail = new CanalEmail(_mockServicioEmail.Object);
            var enviadorNotificaciones = new EnviadorNotificaciones(
                new Mock<ICanalNotificacion>().Object,
                canalEmail,
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

            _mockServicioEmail.Verify(s => s.EnviarAsync(
                It.IsAny<Notificacion>(),
                It.IsAny<Usuario>()),
                Times.Never);
        }

        [Fact]
        public void CanalEmail_EnviaCorrectamente_ConDatosValidos()
        {
            // Arrange
            var usuario = new Usuario
            {
                Id = 1,
                Nombre = "Test User",
                Email = EMAIL_PRUEBA
            };

            var notificacion = new Notificacion
            {
                Id = 1,
                Mensaje = "Mensaje de prueba para email",
                FechaNotificacion = DateTime.Now,
                Estado = EstadoNotificacion.PENDIENTE
            };

            _mockServicioEmail
                .Setup(s => s.EnviarAsync(It.IsAny<Notificacion>(), It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);

            var canalEmail = new CanalEmail(_mockServicioEmail.Object);

            // Act
            canalEmail.EnviarAsync(notificacion, usuario).Wait();

            // Assert
            _mockServicioEmail.Verify(s => s.EnviarAsync(
                It.Is<Notificacion>(n => n.Mensaje == "Mensaje de prueba para email"),
                It.Is<Usuario>(u => u.Email == EMAIL_PRUEBA)),
                Times.Once);
        }

        #endregion

        #region Test de Integración Real (Envía Email real via SendGrid)

        // ESTE TEST ENVÍA UN EMAIL REAL via SendGrid
        // Solo ejecutar manualmente para verificar el flujo completo.
        // Requiere variables de entorno SENDGRID_API_KEY, SENDGRID_FROM_EMAIL configuradas.
        [Fact(Skip = "Ejecutar manualmente - envía email real via SendGrid")]
        //[Fact]
        public void FlujoCompletoReal_MedicionPesoCero_EnviaEmailRealViaSendGrid()
        {
            // seteamos las variables de entorno para SendGrid
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", "TU_API_KEY_AQUI");
            Environment.SetEnvironmentVariable("SENDGRID_FROM_EMAIL", "notificaciones.gestordeapiarios@gmail.com");
            Environment.SetEnvironmentVariable("SENDGRID_FROM_NAME", "Gestor de Apiarios");

            // Arrange
            var usuario = CrearUsuarioDePruebaReal(); // Usar email real para recibir el correo
            var apiario = CrearApiarioDePrueba(usuario);
            var colmena = CrearColmenaDePrueba(apiario);
            var cuadro = CrearCuadroDePrueba(colmena);
            var sensor = CrearSensorDePrueba(colmena, cuadro);

            // ⚠️ FIX: Ensure the full navigation chain is properly linked
            sensor.Colmena = colmena;
            colmena.Apiario = apiario;
            apiario.Usuario = usuario;

            ConfigurarMocksRepositorios(sensor, colmena, cuadro);

            // Usar el servicio REAL de SendGrid
            IServicioEmail servicioEmailReal = new WebApi.Servicios.Notificaciones.ServicioEmail();

            var canalEmail = new CanalEmail(servicioEmailReal);
            var enviadorNotificaciones = new EnviadorNotificaciones(
                new Mock<ICanalNotificacion>().Object,
                canalEmail,
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
            Thread.Sleep(5000); // Más tiempo para email que para SMS

            Assert.True(true, "Email enviado - verificar manualmente en la bandeja de entrada");
        }

        [Fact(Skip = "Ejecutar manualmente - envía email real via SendGrid")]
        //[Fact]
        public void EnvioDirecto_EmailReal_ViaSendGrid()
        {
            // seteamos las variables de entorno para SendGrid
            Environment.SetEnvironmentVariable("SENDGRID_API_KEY", "TU_API_KEY_AQUI");
            Environment.SetEnvironmentVariable("SENDGRID_FROM_EMAIL", "notificaciones.gestordeapiarios@gmail.com");
            Environment.SetEnvironmentVariable("SENDGRID_FROM_NAME", "Gestor de Apiarios");

            // Arrange
            var servicioEmail = new WebApi.Servicios.Notificaciones.ServicioEmail();

            var usuario = new Usuario
            {
                Nombre = "Clara Test",
                Email = "spotifyclara912@gmail.com" // Cambiar por tu email real
            };

            var notificacion = new Notificacion
            {
                Mensaje = "🐝 Test de integración - Notificación del Gestor de Apiarios",
                FechaNotificacion = DateTime.Now,
                Estado = EstadoNotificacion.PENDIENTE
            };

            // Act
            var task = servicioEmail.EnviarAsync(notificacion, usuario);
            task.Wait();

            // Assert
            Assert.True(true, "Email enviado correctamente - verificar bandeja de entrada");
        }

        #endregion

        #region Helper Methods

        private Usuario CrearUsuarioDePrueba()
        {
            return new Usuario("Clara Test", EMAIL_PRUEBA, "password123", "+59891988714", "12", CanalPreferidoNotificacion.EMAIL)
            {
                Id = 1,
                MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.EMAIL
            };
        }

        private Usuario CrearUsuarioDePruebaReal()
        {
            // Para tests reales, cambiar por un email donde puedas verificar la recepción
            return new Usuario("Clara Test", "spotifyclara912@gmail.com", "password123", "+59891988714", "12", CanalPreferidoNotificacion.EMAIL)
            {
                Id = 1,
                MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.EMAIL
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