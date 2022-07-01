using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.Infrastructure.Migrations.AdminDb
{
    public partial class AdminDbRollbackUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminShoppingLists",
                table: "AdminShoppingLists");

            migrationBuilder.RenameTable(
                name: "AdminShoppingLists",
                newName: "ShoppingLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingLists",
                table: "ShoppingLists",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingLists",
                table: "ShoppingLists");

            migrationBuilder.RenameTable(
                name: "ShoppingLists",
                newName: "AdminShoppingLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminShoppingLists",
                table: "AdminShoppingLists",
                column: "Id");
        }
    }
}
