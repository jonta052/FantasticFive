using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Data.Migrations
{
    public partial class KlarnaOrder_userId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlarnaOrders_AspNetUsers_UserId",
                table: "KlarnaOrders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "KlarnaOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KlarnaOrders_AspNetUsers_UserId",
                table: "KlarnaOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KlarnaOrders_AspNetUsers_UserId",
                table: "KlarnaOrders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "KlarnaOrders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_KlarnaOrders_AspNetUsers_UserId",
                table: "KlarnaOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
