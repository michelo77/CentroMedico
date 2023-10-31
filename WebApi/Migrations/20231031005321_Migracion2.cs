using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class Migracion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblEspecialidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEspecialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblProfesionalEspecialidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspecialidadId = table.Column<int>(type: "int", nullable: false),
                    ProfecionalId = table.Column<int>(type: "int", nullable: false),
                    ProfesionalesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProfesionalEspecialidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblProfesionalEspecialidad_TblEspecialidades_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "TblEspecialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblProfesionalEspecialidad_TblProfesionales_ProfesionalesId",
                        column: x => x.ProfesionalesId,
                        principalTable: "TblProfesionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblProfesionalEspecialidad_EspecialidadId",
                table: "TblProfesionalEspecialidad",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProfesionalEspecialidad_ProfesionalesId",
                table: "TblProfesionalEspecialidad",
                column: "ProfesionalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblProfesionalEspecialidad");

            migrationBuilder.DropTable(
                name: "TblEspecialidades");
        }
    }
}
