using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBook.Data.Migrations
{
    public partial class SetRelationshipOrderedBookUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedBooks_Orders_OrderId",
                table: "OrderedBooks");

            migrationBuilder.DropIndex(
                name: "IX_OrderedBooks_OrderId",
                table: "OrderedBooks");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "IsCheckedOut");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "OrderedBooks",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderedBooks",
                newName: "IsOrdered");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "OrderedBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedBooks_ApplicationUserId",
                table: "OrderedBooks",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedBooks_AspNetUsers_ApplicationUserId",
                table: "OrderedBooks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedBooks_AspNetUsers_ApplicationUserId",
                table: "OrderedBooks");

            migrationBuilder.DropIndex(
                name: "IX_OrderedBooks_ApplicationUserId",
                table: "OrderedBooks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "OrderedBooks");

            migrationBuilder.RenameColumn(
                name: "IsCheckedOut",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrderedBooks",
                newName: "Subtotal");

            migrationBuilder.RenameColumn(
                name: "IsOrdered",
                table: "OrderedBooks",
                newName: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedBooks_OrderId",
                table: "OrderedBooks",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedBooks_Orders_OrderId",
                table: "OrderedBooks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
