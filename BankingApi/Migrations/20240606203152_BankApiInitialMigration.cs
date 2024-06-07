using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingApi.Migrations
{
    /// <inheritdoc />
    public partial class BankApiInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientBankDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accounttype = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBankDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientBankDetails_ClientDetails_ClientDetailsId",
                        column: x => x.ClientDetailsId,
                        principalTable: "ClientDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClientDetails",
                columns: new[] { "Id", "Address", "ClientID", "EmailAddress", "Gender", "MobileNumber", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "1 First st Johannesburg", "0123456789012", "sphe@gmail.com", 0, "07123456789", "Sphe", "Ngidi" },
                    { 2, "2 Second st Johannesburg", "2123456789012", "ProD@gmail.com", 1, "07234567890", "Pro", "Dot" },
                    { 3, "23 Second st Johannesburg", "3123456789012", "sphesihle1@gmail.com", 0, "0734567890", "Sphesihle", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "ClientBankDetails",
                columns: new[] { "Id", "AccountNumber", "Accounttype", "Balance", "ClientDetailsId", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "1234567890", 0, 1000m, 1, "Sphe Ngidi Cheque", 0 },
                    { 2, "9876543210", 1, 500m, 2, "Pro Dot Savings", 0 },
                    { 3, "1376543210", 2, 200m, 3, "Sphesihle Smith FixedDeposit", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientBankDetails_ClientDetailsId",
                table: "ClientBankDetails",
                column: "ClientDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientBankDetails");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ClientDetails");
        }
    }
}
