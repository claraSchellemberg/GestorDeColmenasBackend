using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDeDatos.Migrations
{
    /// <inheritdoc />
    public partial class ProbandoRestrinccionesEF3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colmenas_Apiarios_ApiarioId",
                table: "Colmenas");

            migrationBuilder.AddColumn<int>(
                name: "ApiarioId1",
                table: "Colmenas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colmenas_ApiarioId1",
                table: "Colmenas",
                column: "ApiarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Colmenas_Apiarios_ApiarioId",
                table: "Colmenas",
                column: "ApiarioId",
                principalTable: "Apiarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Colmenas_Apiarios_ApiarioId1",
                table: "Colmenas",
                column: "ApiarioId1",
                principalTable: "Apiarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colmenas_Apiarios_ApiarioId",
                table: "Colmenas");

            migrationBuilder.DropForeignKey(
                name: "FK_Colmenas_Apiarios_ApiarioId1",
                table: "Colmenas");

            migrationBuilder.DropIndex(
                name: "IX_Colmenas_ApiarioId1",
                table: "Colmenas");

            migrationBuilder.DropColumn(
                name: "ApiarioId1",
                table: "Colmenas");

            migrationBuilder.AddForeignKey(
                name: "FK_Colmenas_Apiarios_ApiarioId",
                table: "Colmenas",
                column: "ApiarioId",
                principalTable: "Apiarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
