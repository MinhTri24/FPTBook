using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBook.Data.Migrations
{
    public partial class UpdateCategoryRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRequests_AspNetUsers_UserId",
                table: "CategoryRequests");

            migrationBuilder.DropTable(
                name: "OrderOrderedBook");

            migrationBuilder.DropIndex(
                name: "IX_CategoryRequests_UserId",
                table: "CategoryRequests");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "CategoryRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CategoryRequests",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CategoryRequests",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "CategoryRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderOrderedBook",
                columns: table => new
                {
                    OrderedBooksId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrdersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderOrderedBook", x => new { x.OrderedBooksId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_OrderOrderedBook_OrderedBooks_OrderedBooksId",
                        column: x => x.OrderedBooksId,
                        principalTable: "OrderedBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderOrderedBook_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRequests_UserId",
                table: "CategoryRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderOrderedBook_OrdersId",
                table: "OrderOrderedBook",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRequests_AspNetUsers_UserId",
                table: "CategoryRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
