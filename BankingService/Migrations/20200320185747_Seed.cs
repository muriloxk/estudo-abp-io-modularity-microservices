using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingService.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountBalance", "AccountType", "ConcurrencyStamp", "ExtraProperties" },
                values: new object[] { new Guid("7cf8ac09-7f7a-45b1-880d-1234f30fbd84"), 100m, "Poupança", "a50c4f980beb4ebaac30a00a7bf75b7d", "{}" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountBalance", "AccountType", "ConcurrencyStamp", "ExtraProperties" },
                values: new object[] { new Guid("d337dafa-467f-4eb6-a064-58634e6bfdb8"), 200m, "Credito", "180400348f344a20bc42b96c19478ff3", "{}" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("7cf8ac09-7f7a-45b1-880d-1234f30fbd84"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("d337dafa-467f-4eb6-a064-58634e6bfdb8"));
        }
    }
}
