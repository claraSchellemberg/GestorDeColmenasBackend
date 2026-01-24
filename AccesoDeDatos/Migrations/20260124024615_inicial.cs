using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDeDatos.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracions",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracions", x => x.Nombre);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedioDeComunicacionDePreferencia = table.Column<int>(type: "int", nullable: false),
                    NumeroApicultor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apiarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Latitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UbicacionDeReferencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apiarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apiarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Colmenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInstalacionSensores = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Condicion = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    ApiarioId = table.Column<int>(type: "int", nullable: false),
                    UltimaMedicionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colmenas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colmenas_Apiarios_ApiarioId",
                        column: x => x.ApiarioId,
                        principalTable: "Apiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cuadros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColmenaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuadros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuadros_Colmenas_ColmenaId",
                        column: x => x.ColmenaId,
                        principalTable: "Colmenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicionColmenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempExterna = table.Column<float>(type: "real", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    FechaMedicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ColmenaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicionColmenas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicionColmenas_Colmenas_ColmenaId",
                        column: x => x.ColmenaId,
                        principalTable: "Colmenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensores",
                columns: table => new
                {
                    SensorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoSensor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColmenaId = table.Column<int>(type: "int", nullable: false),
                    CuadroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensores", x => x.SensorId);
                    table.ForeignKey(
                        name: "FK_Sensores_Colmenas_ColmenaId",
                        column: x => x.ColmenaId,
                        principalTable: "Colmenas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sensores_Cuadros_CuadroId",
                        column: x => x.CuadroId,
                        principalTable: "Cuadros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SensorPorCuadros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempInterna1 = table.Column<float>(type: "real", nullable: false),
                    TempInterna2 = table.Column<float>(type: "real", nullable: false),
                    TempInterna3 = table.Column<float>(type: "real", nullable: false),
                    FechaMedicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    CuadroId = table.Column<int>(type: "int", nullable: false),
                    CuadroId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorPorCuadros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorPorCuadros_Cuadros_CuadroId",
                        column: x => x.CuadroId,
                        principalTable: "Cuadros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SensorPorCuadros_Cuadros_CuadroId1",
                        column: x => x.CuadroId1,
                        principalTable: "Cuadros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SensorPorCuadros_Sensores_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensores",
                        principalColumn: "SensorId");
                });

            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstaPendiente = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    MedicionColmenaId = table.Column<int>(type: "int", nullable: true),
                    ValorEstaEnRangoBorde = table.Column<bool>(type: "bit", nullable: true),
                    MensajesAlerta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SensorPorCuadroId = table.Column<int>(type: "int", nullable: true),
                    RegistroSensor_ValorEstaEnRangoBorde = table.Column<bool>(type: "bit", nullable: true),
                    RegistroSensor_MensajesAlerta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registros_MedicionColmenas_MedicionColmenaId",
                        column: x => x.MedicionColmenaId,
                        principalTable: "MedicionColmenas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Registros_SensorPorCuadros_SensorPorCuadroId",
                        column: x => x.SensorPorCuadroId,
                        principalTable: "SensorPorCuadros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNotificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistroAsociadoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioReceptorId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Registros_RegistroAsociadoId",
                        column: x => x.RegistroAsociadoId,
                        principalTable: "Registros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_UsuarioReceptorId",
                        column: x => x.UsuarioReceptorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apiario_UsuarioId_Nombre_Unique",
                table: "Apiarios",
                columns: new[] { "UsuarioId", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colmena_ApiarioId_Nombre_Unique",
                table: "Colmenas",
                columns: new[] { "ApiarioId", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colmenas_UltimaMedicionId",
                table: "Colmenas",
                column: "UltimaMedicionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuadros_ColmenaId",
                table: "Cuadros",
                column: "ColmenaId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicionColmenas_ColmenaId",
                table: "MedicionColmenas",
                column: "ColmenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_RegistroAsociadoId",
                table: "Notificaciones",
                column: "RegistroAsociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioReceptorId",
                table: "Notificaciones",
                column: "UsuarioReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_MedicionColmenaId",
                table: "Registros",
                column: "MedicionColmenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_SensorPorCuadroId",
                table: "Registros",
                column: "SensorPorCuadroId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensores_ColmenaId",
                table: "Sensores",
                column: "ColmenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensores_CuadroId",
                table: "Sensores",
                column: "CuadroId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorPorCuadros_CuadroId",
                table: "SensorPorCuadros",
                column: "CuadroId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorPorCuadros_CuadroId1",
                table: "SensorPorCuadros",
                column: "CuadroId1",
                unique: true,
                filter: "[CuadroId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SensorPorCuadros_SensorId",
                table: "SensorPorCuadros",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colmenas_MedicionColmenas_UltimaMedicionId",
                table: "Colmenas",
                column: "UltimaMedicionId",
                principalTable: "MedicionColmenas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apiarios_Usuarios_UsuarioId",
                table: "Apiarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Colmenas_Apiarios_ApiarioId",
                table: "Colmenas");

            migrationBuilder.DropForeignKey(
                name: "FK_Colmenas_MedicionColmenas_UltimaMedicionId",
                table: "Colmenas");

            migrationBuilder.DropTable(
                name: "Configuracions");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Registros");

            migrationBuilder.DropTable(
                name: "SensorPorCuadros");

            migrationBuilder.DropTable(
                name: "Sensores");

            migrationBuilder.DropTable(
                name: "Cuadros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Apiarios");

            migrationBuilder.DropTable(
                name: "MedicionColmenas");

            migrationBuilder.DropTable(
                name: "Colmenas");
        }
    }
}
