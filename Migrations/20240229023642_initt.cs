using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exa.Migrations
{
    /// <inheritdoc />
    public partial class initt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Foros",
                table: "Foros");

            migrationBuilder.RenameTable(
                name: "Foros",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Foros");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foros",
                table: "Foros",
                column: "Id");
        }
    }
}
