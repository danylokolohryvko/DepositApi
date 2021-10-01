using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DepositApi.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Deposits",
                columns: c => new
                {
                    Id = c.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Amount = c.Column<decimal>(nullable: false),
                    Term = c.Column<int>(nullable: false),
                    Date = c.Column<DateTime>(nullable: false),
                    Percent = c.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                });

            migrationBuilder.CreateTable("DepositCalcs",
                columns: c => new
                {
                    Id = c.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Month = c.Column<int>(nullable: false),
                    PercentAdded = c.Column<decimal>(nullable: false),
                    TotalAmount = c.Column<decimal>(nullable: false),
                    DepositId = c.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositCalcs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Deposits");
            migrationBuilder.DropTable("DepositCalcs");
        }
    }
}
