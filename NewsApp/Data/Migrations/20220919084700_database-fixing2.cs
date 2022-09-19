using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.Data.Migrations
{
    public partial class databasefixing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "SubscriptionTypes",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTypeId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                table: "Subscriptions",
                column: "SubscriptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId",
                table: "Subscriptions",
                column: "SubscriptionTypeId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id");
        }
    }
}
