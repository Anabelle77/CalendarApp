using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarApp.Migrations
{
    /// <inheritdoc />
    public partial class VinculoEventoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Eventos");

            migrationBuilder.AddColumn<int>(
                name: "EventoModelId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Eventos",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eventos",
                table: "Eventos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EventoModelId",
                table: "Usuarios",
                column: "EventoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_UsuarioId",
                table: "Eventos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Usuarios_UsuarioId",
                table: "Eventos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Eventos_EventoModelId",
                table: "Usuarios",
                column: "EventoModelId",
                principalTable: "Eventos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Usuarios_UsuarioId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Eventos_EventoModelId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EventoModelId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eventos",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_UsuarioId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "EventoModelId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Eventos");

            migrationBuilder.RenameTable(
                name: "Eventos",
                newName: "Events");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");
        }
    }
}
