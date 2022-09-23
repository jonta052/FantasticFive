using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Data.Migrations
{
    public partial class databasefixing3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionTypes_Subscriptions_SubscriptionId",
                table: "SubscriptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionTypes_SubscriptionId",
                table: "SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "KlarnaOrder",
                table: "Subscriptions",
                newName: "Active");

            migrationBuilder.AddColumn<int>(
                name: "KlarnaOrderId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTypeId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KlarnaOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlarnaOrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    NumberOfInstallments = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FraudStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlarnaOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KlarnaOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_KlarnaOrderId",
                table: "Subscriptions",
                column: "KlarnaOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                table: "Subscriptions",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KlarnaOrders_UserId",
                table: "KlarnaOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_KlarnaOrders_KlarnaOrderId",
                table: "Subscriptions",
                column: "KlarnaOrderId",
                principalTable: "KlarnaOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId",
                table: "Subscriptions",
                column: "SubscriptionTypeId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_KlarnaOrders_KlarnaOrderId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "KlarnaOrders");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_KlarnaOrderId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "KlarnaOrderId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Subscriptions",
                newName: "KlarnaOrder");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "SubscriptionTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTypes_SubscriptionId",
                table: "SubscriptionTypes",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionTypes_Subscriptions_SubscriptionId",
                table: "SubscriptionTypes",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }
    }
}
