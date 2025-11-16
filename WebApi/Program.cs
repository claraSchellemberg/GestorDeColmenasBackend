using AccesoDeDatos.Repositorios.EF;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.CasosDeUso.Apiarios;
using LogicaDeServicios.CasosDeUso.Registros;
using LogicaDeServicios.DTOs.Apiarios;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Inyecciones para los Casos de Uso de Apiarios
builder.Services.AddScoped<IAgregar<ApiarioSetDto>, AgregarApiario>();
builder.Services.AddScoped<IObtenerPorId<ApiarioGetDto>, ObtenerPorIdApiario>();
builder.Services.AddScoped<IObtenerTodos<ApiarioGetDto>, ObtenerTodosApiarios>();

//Inyecciones para los Casos de Uso de Registro
builder.Services.AddScoped<IAgregar<RegistroSetDto>, AgregarRegistro>();
builder.Services.AddScoped<IObtenerPorId<RegistroGetDto>, ObtenerPorIdRegistro>();
builder.Services.AddScoped<IObtenerTodos<RegistroGetDto>, ObtenerTodosRegistros>();


// Inyecciones para los repositorios
builder.Services.AddScoped<IRepositorioRegistro, RepositorioRegistro>();
builder.Services.AddScoped<IRepositorioApiario, RepositorioApiario>();

// Inyecta el contex y la cadena de conexion que la toma desde el json
//.Services.AddDbContext<GestorContext>();
// Add services to the container.
builder.Services.AddDbContext<GestorContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
