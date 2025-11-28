using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDeDatos.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_Colmenas_ColmenaId",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_ColmenaId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "ColmenaId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "NombreColmena",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "TempExterna",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "TempInterna1",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "TempInterna2",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "TempInterna3",
                table: "Registros");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Registros",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cuadros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColmenaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuadros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuadros_Colmenas_ColmenaId",
                        column: x => x.ColmenaId,
                        principalTable: "Colmenas",
                        principalColumn: "Id");
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
                    TipoSensor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensores", x => x.SensorId);
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
                    FechaMedicion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorPorCuadros", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuadros_ColmenaId",
                table: "Cuadros",
                column: "ColmenaId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicionColmenas_ColmenaId",
                table: "MedicionColmenas",
                column: "ColmenaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuadros");

            migrationBuilder.DropTable(
                name: "MedicionColmenas");

            migrationBuilder.DropTable(
                name: "Sensores");

            migrationBuilder.DropTable(
                name: "SensorPorCuadros");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Registros");

            migrationBuilder.AddColumn<int>(
                name: "ColmenaId",
                table: "Registros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreColmena",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Peso",
                table: "Registros",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TempExterna",
                table: "Registros",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TempInterna1",
                table: "Registros",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TempInterna2",
                table: "Registros",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TempInterna3",
                table: "Registros",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Registros_ColmenaId",
                table: "Registros",
                column: "ColmenaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_Colmenas_ColmenaId",
                table: "Registros",
                column: "ColmenaId",
                principalTable: "Colmenas",
                principalColumn: "Id");
        }
    }
}
