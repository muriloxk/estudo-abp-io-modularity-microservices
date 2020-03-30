using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingService.Migrations
{
    public partial class Multitenancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("7cf8ac09-7f7a-45b1-880d-1234f30fbd84"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("d337dafa-467f-4eb6-a064-58634e6bfdb8"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Accounts",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountBalance", "AccountType", "ConcurrencyStamp", "ExtraProperties", "TenantId" },
                values: new object[,]
                {
                    { new Guid("c622431b-8807-4e0d-bf53-a9ff0fdbd1cc"), 100m, "Poupança", "ee269e17ac3844fd8c818b5f3ccc79f5", "{}", new Guid("9fcc4ade-21e5-4686-a1ba-36aff25e5d0f") },
                    { new Guid("4f4afd13-c5a8-4b7c-9b7a-cd351f9bdcb6"), 200m, "Credito", "c094c90e30cb4464a210723712c9388f", "{}", new Guid("9fcc4ade-21e5-4686-a1ba-36aff25e5d0f") },
                    { new Guid("2189d7d3-a8b6-45a6-bada-3a8acef4bc94"), 100m, "Poupança", "2cac11a60e404e0f9f39f51f6192027f", "{}", new Guid("68d3a738-2918-4b1d-a293-71e9aaff8024") },
                    { new Guid("ba5d66de-3828-496b-bffe-7829c5cc103a"), 200m, "Credito", "f265c88809cb4eb59439015c093f416c", "{}", new Guid("68d3a738-2918-4b1d-a293-71e9aaff8024") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("2189d7d3-a8b6-45a6-bada-3a8acef4bc94"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("4f4afd13-c5a8-4b7c-9b7a-cd351f9bdcb6"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("ba5d66de-3828-496b-bffe-7829c5cc103a"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c622431b-8807-4e0d-bf53-a9ff0fdbd1cc"));

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountBalance", "AccountType", "ConcurrencyStamp", "ExtraProperties" },
                values: new object[] { new Guid("7cf8ac09-7f7a-45b1-880d-1234f30fbd84"), 100m, "Poupança", "a50c4f980beb4ebaac30a00a7bf75b7d", "{}" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountBalance", "AccountType", "ConcurrencyStamp", "ExtraProperties" },
                values: new object[] { new Guid("d337dafa-467f-4eb6-a064-58634e6bfdb8"), 200m, "Credito", "180400348f344a20bc42b96c19478ff3", "{}" });
        }
    }
}
