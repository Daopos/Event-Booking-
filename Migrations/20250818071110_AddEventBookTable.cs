using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventBooking.Migrations
{
    /// <inheritdoc />
    public partial class AddEventBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventBook_AspNetUsers_userId",
                table: "EventBook");

            migrationBuilder.DropForeignKey(
                name: "FK_EventBook_Events_EventId",
                table: "EventBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventBook",
                table: "EventBook");

            migrationBuilder.RenameTable(
                name: "EventBook",
                newName: "EventBooks");

            migrationBuilder.RenameIndex(
                name: "IX_EventBook_userId",
                table: "EventBooks",
                newName: "IX_EventBooks_userId");

            migrationBuilder.RenameIndex(
                name: "IX_EventBook_EventId",
                table: "EventBooks",
                newName: "IX_EventBooks_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventBooks",
                table: "EventBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventBooks_AspNetUsers_userId",
                table: "EventBooks",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventBooks_Events_EventId",
                table: "EventBooks",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventBooks_AspNetUsers_userId",
                table: "EventBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_EventBooks_Events_EventId",
                table: "EventBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventBooks",
                table: "EventBooks");

            migrationBuilder.RenameTable(
                name: "EventBooks",
                newName: "EventBook");

            migrationBuilder.RenameIndex(
                name: "IX_EventBooks_userId",
                table: "EventBook",
                newName: "IX_EventBook_userId");

            migrationBuilder.RenameIndex(
                name: "IX_EventBooks_EventId",
                table: "EventBook",
                newName: "IX_EventBook_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventBook",
                table: "EventBook",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventBook_AspNetUsers_userId",
                table: "EventBook",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventBook_Events_EventId",
                table: "EventBook",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
