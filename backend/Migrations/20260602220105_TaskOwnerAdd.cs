using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class TaskOwnerAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_OwnerId",
                table: "TaskItems",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_AspNetUsers_OwnerId",
                table: "TaskItems",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_AspNetUsers_OwnerId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_OwnerId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "TaskItems");
        }
    }
}
