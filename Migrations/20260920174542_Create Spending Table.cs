using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendTracerApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateSpendingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spending",
                columns: table => new
                {
                    SpendingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpendingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpendingDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpendingValue = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spending", x => x.SpendingId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spending");
        }
    }
}
