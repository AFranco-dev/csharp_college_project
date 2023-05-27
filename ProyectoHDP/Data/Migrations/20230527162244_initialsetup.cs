using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoHDP.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipoContrato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    salario = table.Column<double>(type: "float", nullable: false),
                    fechaContrato = table.Column<DateTime>(type: "date", nullable: false),
                    fechaRenuncia = table.Column<DateTime>(type: "date", nullable: false),
                    fechaEmision = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.iD);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");
        }
    }
}
