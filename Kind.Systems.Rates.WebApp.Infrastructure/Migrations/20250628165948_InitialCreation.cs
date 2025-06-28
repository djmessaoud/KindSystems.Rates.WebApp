using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kind.Systems.Rates.WebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Base = table.Column<string>(type: "TEXT", nullable: false),
                    Quote = table.Column<string>(type: "TEXT", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false),
                    RetrievedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_Base_Quote",
                table: "Rates",
                columns: new[] { "Base", "Quote" });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_RetrievedAt",
                table: "Rates",
                column: "RetrievedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
