using AccesoDeDatos.Repositorios.EF;
using LogicaDeNegocios;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.Notificaciones;
using LogicaDeNegocios.InterfacesRepositorio.Registros;
using LogicaDeServicios.CasosDeUso.Apiarios;
using LogicaDeServicios.CasosDeUso.Colmenas;
using LogicaDeServicios.CasosDeUso.Notificaciones;
using LogicaDeServicios.CasosDeUso.Notificaciones.Canales;
using LogicaDeServicios.CasosDeUso.TomarMedicion;
using LogicaDeServicios.CasosDeUso.Usuarios;
using LogicaDeServicios.DTOs.Apiarios;
using LogicaDeServicios.DTOs.Arduino;
using LogicaDeServicios.DTOs.Colmenas;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.DTOs.Usuarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.EntityFrameworkCore;
using WebApi.Hubs;
using WebApi.Servicios.Notificaciones;



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
builder.Services.AddScoped<IObtenerPorIdUsuario<IEnumerable<ApiarioGetDto>>, ObtenerApiarioPorIdUsuario>();

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
builder.Services.AddScoped<IObtenerRegistrosPorColmena<RegistroPorColmenaDto>, ObtenerRegistrosPorColmena>();

//Inyecciones para los Casos de Uso de Registro
builder.Services.AddScoped<IAgregar<DataArduinoDto, DataArduinoDto>, AgregarMedicion>();

//Inyecciones para los Casos de Uso de Usuario
builder.Services.AddScoped<IAgregar<UsuarioSetDto, UsuarioGetDto>, AgregarUsuario>();
builder.Services.AddScoped<IObtenerPorId<UsuarioGetDto>, ObtenerPorIdUsuario>();
builder.Services.AddScoped<IActualizar<UsuarioSetDto>, ActualizarUsuario>();
builder.Services.AddScoped<IEliminar, EliminarUsuario>();
builder.Services.AddScoped<ILogin<UsuarioLoginDto>, ObtenerLogin>();

//Inyeccion para las notificaciones
// Servicio de infraestructura
builder.Services.AddScoped<IServicioSms, VonageServicioSms>();
builder.Services.AddScoped<IServicioEmail, ServicioEmail>();
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

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                       ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");


// Inyecta el contex y la cadena de conexion que la toma desde el json
builder.Services.AddDbContext<GestorContext>(options =>
    options.UseSqlServer(
        connectionString,
        sql => sql.CommandTimeout(180) // 3 minutos
    )
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
