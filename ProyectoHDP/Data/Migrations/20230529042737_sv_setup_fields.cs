using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoHDP.Data.Migrations
{
    public partial class sv_setup_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DUI",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cargo",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "correo",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "correoEmpresa",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "direccion",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "direccionEmpresa",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nISSS",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nTelefono",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "telefonoEmpresa",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DUI",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "cargo",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "correo",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "correoEmpresa",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "direccion",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "direccionEmpresa",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "nISSS",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "nTelefono",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "telefonoEmpresa",
                table: "Empleados");
        }
    }
}
