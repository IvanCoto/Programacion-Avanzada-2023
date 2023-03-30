using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ClienteToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Canton = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Distrito = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
