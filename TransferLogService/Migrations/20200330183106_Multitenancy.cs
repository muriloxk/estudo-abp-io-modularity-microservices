using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransferLogService.Migrations
{
    public partial class Multitenancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransferLogs",
                keyColumn: "Id",
                keyValue: new Guid("168cd017-280a-4b38-913a-dd3426d88058"));

            migrationBuilder.DeleteData(
                table: "TransferLogs",
                keyColumn: "Id",
                keyValue: new Guid("77dbd3f9-6dde-484a-b4d0-c725c4b998d5"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TransferLogs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "TransferLogs",
                columns: new[] { "Id", "ConcurrencyStamp", "ExtraProperties", "FromAccount", "TenantId", "ToAccount", "TransferAmount" },
                values: new object[,]
                {
                    { new Guid("af46fed8-d7af-4d54-9b48-1700423828d1"), "90cd2d72401f4a4791e45a1dbee9513f", "{}", "1231231201-313-131", new Guid("9fcc4ade-21e5-4686-a1ba-36aff25e5d0f"), "1231231201-313-132", 100m },
                    { new Guid("564a6dc1-6988-46e3-8a5a-7d31e90c5288"), "94f1f43e6af84a08a2c661f4ecf04160", "{}", "1231231201-313-131", new Guid("9fcc4ade-21e5-4686-a1ba-36aff25e5d0f"), "1231231201-313-132", 100m },
                    { new Guid("248b01a6-8e1e-42bf-804b-284eaf9705dd"), "c67f74de4b4043a58df1ebaf02b8e2db", "{}", "1231231201-313-131", new Guid("68d3a738-2918-4b1d-a293-71e9aaff8024"), "1231231201-313-132", 100m },
                    { new Guid("03eb804c-deba-47a1-ba69-7c9f18983d46"), "e673663a8c744993a4a257ef5d776b76", "{}", "1231231201-313-131", new Guid("68d3a738-2918-4b1d-a293-71e9aaff8024"), "1231231201-313-132", 100m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransferLogs",
                keyColumn: "Id",
                keyValue: new Guid("03eb804c-deba-47a1-ba69-7c9f18983d46"));

            migrationBuilder.DeleteData(
                table: "TransferLogs",
                keyColumn: "Id",
                keyValue: new Guid("248b01a6-8e1e-42bf-804b-284eaf9705dd"));

            migrationBuilder.DeleteData(
                table: "TransferLogs",
                keyColumn: "Id",
                keyValue: new Guid("564a6dc1-6988-46e3-8a5a-7d31e90c5288"));

            migrationBuilder.DeleteData(
                table: "TransferLogs",
                keyColumn: "Id",
                keyValue: new Guid("af46fed8-d7af-4d54-9b48-1700423828d1"));

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TransferLogs");

            migrationBuilder.InsertData(
                table: "TransferLogs",
                columns: new[] { "Id", "ConcurrencyStamp", "ExtraProperties", "FromAccount", "ToAccount", "TransferAmount" },
                values: new object[] { new Guid("168cd017-280a-4b38-913a-dd3426d88058"), "1ac2a725176549f487524e9cd00bc734", "{}", "1231231201-313-131", "1231231201-313-132", 100m });

            migrationBuilder.InsertData(
                table: "TransferLogs",
                columns: new[] { "Id", "ConcurrencyStamp", "ExtraProperties", "FromAccount", "ToAccount", "TransferAmount" },
                values: new object[] { new Guid("77dbd3f9-6dde-484a-b4d0-c725c4b998d5"), "5e5129aa7ec4405d914f858f522b3c77", "{}", "1231231201-313-131", "1231231201-313-132", 100m });
        }
    }
}
