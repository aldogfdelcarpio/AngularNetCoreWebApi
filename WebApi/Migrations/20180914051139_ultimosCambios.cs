using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class ultimosCambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tipoUsuario",
                table: "Usuarios",
                newName: "idTipoUsuario");

            migrationBuilder.RenameColumn(
                name: "diaSemana",
                table: "Charlas",
                newName: "Fecha");

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    cantidadMaximaCharlas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idTipoUsuario",
                table: "Usuarios",
                column: "idTipoUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_TipoUsuario_idTipoUsuario",
                table: "Usuarios",
                column: "idTipoUsuario",
                principalTable: "TipoUsuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_TipoUsuario_idTipoUsuario",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "TipoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_idTipoUsuario",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "idTipoUsuario",
                table: "Usuarios",
                newName: "tipoUsuario");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Charlas",
                newName: "diaSemana");
        }
    }
}
