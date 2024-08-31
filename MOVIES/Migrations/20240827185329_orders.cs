using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOVIES.Migrations
{
    /// <inheritdoc />
    public partial class orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "orderId",
                table: "MovieCarts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCarts_orderId",
                table: "MovieCarts",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ApplicationUserId",
                table: "orders",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCarts_orders_orderId",
                table: "MovieCarts",
                column: "orderId",
                principalTable: "orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCarts_orders_orderId",
                table: "MovieCarts");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropIndex(
                name: "IX_MovieCarts_orderId",
                table: "MovieCarts");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "MovieCarts");
        }
    }
}
