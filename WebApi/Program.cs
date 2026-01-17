using AccesoDeDatos.Repositorios.EF;
using LogicaDeNegocios;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.CasosDeUso.Apiarios;
using LogicaDeServicios.CasosDeUso.Colmenas;
using LogicaDeServicios.CasosDeUso.Notificaciones;
using LogicaDeServicios.CasosDeUso.Notificaciones.Canales;
using LogicaDeServicios.CasosDeUso.TomarMedicion;
using LogicaDeServicios.DTOs.Apiarios;
using LogicaDeServicios.DTOs.Arduino;
using LogicaDeServicios.DTOs.Colmenas;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.EntityFrameworkCore;
using WebApi.Hubs;
using WebApi.Servicios.Notificaciones;
using WebApi.Servicios.Sms;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SignalR
builder.Services.AddSignalR();

// Inyecciones para los repositorios
builder.Services.AddScoped<IRepositorioApiario, RepositorioApiario>();
builder.Services.AddScoped<IRepositorioColmena, RepositorioColmena>();
builder.Services.AddScoped<IRepositorioCuadro, RepositorioCuadro>();
builder.Services.AddScoped<IRepositorioNotificacion, RepositorioNotificaciones>();
builder.Services.AddScoped<IRepositorioRegistro, RepositorioRegistro>();
builder.Services.AddScoped<IRepositorioRegistroMedicionColmena, RepositorioRegistroMedicionColmena>();
builder.Services.AddScoped<IRepositorioRegistroSensor, RepositorioRegistroSensor>();
builder.Services.AddScoped<IRepositorioSensor, RepositorioSensor>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

//Inyecciones para los Casos de Uso de Apiarios
builder.Services.AddScoped<IAgregar<ApiarioSetDto, ApiarioGetDto>, AgregarApiario>();
builder.Services.AddScoped<IObtenerPorId<ApiarioGetDto>, ObtenerPorIdApiario>();
builder.Services.AddScoped<IObtenerTodos<ApiarioGetDto>, ObtenerTodosApiarios>();
builder.Services.AddScoped<IActualizar<ApiarioSetDto>, ActualizarApiario>();
builder.Services.AddScoped<EliminarApiario>();

//Inyecciones para los Casos de Uso de Colmenas
builder.Services.AddScoped<IAgregar<ColmenaSetDto, ColmenaGetDto>>(sp =>
    new AgregarColmena(
        sp.GetRequiredService<IRepositorioColmena>(),
        sp.GetRequiredService<IRepositorioApiario>()
    )
);
builder.Services.AddScoped<IObtenerPorId<ColmenaGetDto>, ObtenerPorIdColmena>();
builder.Services.AddScoped<IObtenerTodos<ColmenaGetDto>, ObtenerTodosColmenas>();
builder.Services.AddScoped<IObtenerColmenasPorApiario<ColmenaGetDto>, ObtenerColmenasPorApiario>();
builder.Services.AddScoped<IObtenerColmenasPorApiario<ColmenaGetDto>>(sp =>
    new ObtenerColmenasPorApiario(
        sp.GetRequiredService<IRepositorioColmena>(),
        sp.GetRequiredService<IRepositorioApiario>()
    )
);
builder.Services.AddScoped<IActualizar<ColmenaSetDto>, ActualizarColmena>();
builder.Services.AddScoped<IObtenerDetalleColmena<DetalleColmenaDto>, ObtenerDetalleColmena>();
builder.Services.AddScoped<EliminarColmena>();
builder.Services.AddScoped<IObtenerPorNombreApiarioEIdUsuario<ApiarioGetDto>, ObtenerApiarioPorNombreEIdUsuario>();

//Inyecciones para los Casos de Uso de Registro
builder.Services.AddScoped<IAgregar<DataArduinoDto, DataArduinoDto>, AgregarMedicion>();

//Inyeccion para las notificaciones
// Servicio de infraestructura
builder.Services.AddScoped<IServicioSms, VonageServicioSms>();

// Servicio de push para SignalR
builder.Services.AddScoped<INotificacionPushService, SignalRNotificacionPushService>();

// Canales de notificación
builder.Services.AddScoped<CanalSms>();
builder.Services.AddScoped<CanalEmail>();
builder.Services.AddScoped<CanalWhatsApp>();
builder.Services.AddScoped<CanalFrontend>();

// EnviadorNotificaciones (observer)
builder.Services.AddScoped<EnviadorNotificaciones>(provider =>
    new EnviadorNotificaciones(
        provider.GetRequiredService<CanalSms>(),
        provider.GetRequiredService<CanalEmail>(),
        provider.GetRequiredService<CanalWhatsApp>(),
        provider.GetRequiredService<CanalFrontend>()
    ));

// GeneradorNotificaciones con observer suscrito
builder.Services.AddScoped<IGeneradorNotificaciones>(provider =>
{
    var generador = new GeneradorNotificaciones();
    var enviador = provider.GetRequiredService<EnviadorNotificaciones>();
    generador.SuscribirObservador(enviador);
    return generador;
});

// Inyecta el contex y la cadena de conexion que la toma desde el json
builder.Services.AddDbContext<GestorContext>(
    options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GestorContext>();
    var configs = context.Configuracions
        .ToList()
        .Select(c => (c.Nombre, c.Valor))
        .ToList();
    Configuracion.Inicializar(configs);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map SignalR Hub
app.MapHub<NotificacionHub>("/notificacionHub");

app.Run();
