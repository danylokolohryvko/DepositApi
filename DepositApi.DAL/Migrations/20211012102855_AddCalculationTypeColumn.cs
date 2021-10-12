using Microsoft.EntityFrameworkCore.Migrations;

namespace DepositApi.DAL.Migrations
{
    public partial class AddCalculationTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalculationType",
                table: "Deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculationType",
                table: "Deposits");
        }
    }
}
