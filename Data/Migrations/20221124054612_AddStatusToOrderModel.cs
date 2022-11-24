using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBook.Data.Migrations
{
    public partial class AddStatusToOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
