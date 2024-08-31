using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOVIES.Migrations
{
    /// <inheritdoc />
    public partial class movie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovietId",
                table: "MovieCarts");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ApplicationUserVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RoleVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleVM", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleVM");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ApplicationUserVM");

            migrationBuilder.AddColumn<int>(
                name: "MovietId",
                table: "MovieCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
