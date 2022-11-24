using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTBook.Data.Migrations
{
    public partial class SetRelationshipOrderOrderedBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Orders",
                newName: "CreatedAt");

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
                name: "IX_OrderOrderedBook_OrdersId",
                table: "OrderOrderedBook",
                column: "OrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderOrderedBook");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Orders",
                newName: "CreateAt");
        }
    }
}
