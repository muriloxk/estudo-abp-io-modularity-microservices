using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransferLogService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    FromAccount = table.Column<string>(nullable: true),
                    ToAccount = table.Column<string>(nullable: true),
                    TransferAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferLogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TransferLogs",
                columns: new[] { "Id", "ConcurrencyStamp", "ExtraProperties", "FromAccount", "ToAccount", "TransferAmount" },
                values: new object[] { new Guid("168cd017-280a-4b38-913a-dd3426d88058"), "1ac2a725176549f487524e9cd00bc734", "{}", "1231231201-313-131", "1231231201-313-132", 100m });

            migrationBuilder.InsertData(
                table: "TransferLogs",
                columns: new[] { "Id", "ConcurrencyStamp", "ExtraProperties", "FromAccount", "ToAccount", "TransferAmount" },
                values: new object[] { new Guid("77dbd3f9-6dde-484a-b4d0-c725c4b998d5"), "5e5129aa7ec4405d914f858f522b3c77", "{}", "1231231201-313-131", "1231231201-313-132", 100m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferLogs");
        }
    }
}
