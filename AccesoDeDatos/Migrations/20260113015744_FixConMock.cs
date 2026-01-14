using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDeDatos.Migrations
{
    /// <inheritdoc />
    public partial class FixConMock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sensorPorCuadroId",
                table: "Registros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registros_sensorPorCuadroId",
                table: "Registros",
                column: "sensorPorCuadroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registros_SensorPorCuadros_sensorPorCuadroId",
                table: "Registros",
                column: "sensorPorCuadroId",
                principalTable: "SensorPorCuadros",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registros_SensorPorCuadros_sensorPorCuadroId",
                table: "Registros");

            migrationBuilder.DropIndex(
                name: "IX_Registros_sensorPorCuadroId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "sensorPorCuadroId",
                table: "Registros");
        }
    }
}
