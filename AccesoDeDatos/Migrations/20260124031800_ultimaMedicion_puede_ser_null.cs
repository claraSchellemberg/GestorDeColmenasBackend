using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDeDatos.Migrations
{
    /// <inheritdoc />
    public partial class ultimaMedicion_puede_ser_null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorPorCuadros_Cuadros_CuadroId1",
                table: "SensorPorCuadros");

            migrationBuilder.DropIndex(
                name: "IX_SensorPorCuadros_CuadroId1",
                table: "SensorPorCuadros");

            migrationBuilder.DropColumn(
                name: "CuadroId1",
                table: "SensorPorCuadros");

            migrationBuilder.AddColumn<int>(
                name: "UltimaMedicionId",
                table: "Cuadros",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UltimaMedicionId",
                table: "Colmenas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Cuadros_UltimaMedicionId",
                table: "Cuadros",
                column: "UltimaMedicionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuadros_SensorPorCuadros_UltimaMedicionId",
                table: "Cuadros",
                column: "UltimaMedicionId",
                principalTable: "SensorPorCuadros",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuadros_SensorPorCuadros_UltimaMedicionId",
                table: "Cuadros");

            migrationBuilder.DropIndex(
                name: "IX_Cuadros_UltimaMedicionId",
                table: "Cuadros");

            migrationBuilder.DropColumn(
                name: "UltimaMedicionId",
                table: "Cuadros");

            migrationBuilder.AddColumn<int>(
                name: "CuadroId1",
                table: "SensorPorCuadros",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UltimaMedicionId",
                table: "Colmenas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SensorPorCuadros_CuadroId1",
                table: "SensorPorCuadros",
                column: "CuadroId1",
                unique: true,
                filter: "[CuadroId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorPorCuadros_Cuadros_CuadroId1",
                table: "SensorPorCuadros",
                column: "CuadroId1",
                principalTable: "Cuadros",
                principalColumn: "Id");
        }
    }
}
