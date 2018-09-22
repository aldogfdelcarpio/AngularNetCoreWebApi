using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Capacidad = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    NombreUsuario = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    tipoUsuario = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Charlas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    diaSemana = table.Column<DateTime>(nullable: false),
                    HoraInicio = table.Column<TimeSpan>(nullable: false),
                    HoraFin = table.Column<TimeSpan>(nullable: false),
                    idSala = table.Column<int>(nullable: false),
                    idSpeaker = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charlas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charlas_Salas_idSala",
                        column: x => x.idSala,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Charlas_Usuarios_idSpeaker",
                        column: x => x.idSpeaker,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CharlaDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idCharla = table.Column<int>(nullable: false),
                    idAsistente = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharlaDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharlaDetalles_Usuarios_idAsistente",
                        column: x => x.idAsistente,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CharlaDetalles_Charlas_idCharla",
                        column: x => x.idCharla,
                        principalTable: "Charlas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharlaDetalles_idAsistente",
                table: "CharlaDetalles",
                column: "idAsistente");

            migrationBuilder.CreateIndex(
                name: "IX_CharlaDetalles_idCharla",
                table: "CharlaDetalles",
                column: "idCharla");

            migrationBuilder.CreateIndex(
                name: "IX_Charlas_idSala",
                table: "Charlas",
                column: "idSala");

            migrationBuilder.CreateIndex(
                name: "IX_Charlas_idSpeaker",
                table: "Charlas",
                column: "idSpeaker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharlaDetalles");

            migrationBuilder.DropTable(
                name: "Charlas");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
