using Microsoft.EntityFrameworkCore.Migrations;

namespace DepositApi.DAL.Migrations
{
    public partial class ChangeModelTypesToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>("Amount", "Deposits");
            migrationBuilder.AlterColumn<decimal>("Percent", "Deposits");
            migrationBuilder.AlterColumn<decimal>("TotalAmount", "DepositCalcs");
            migrationBuilder.AlterColumn<decimal>("PercentAdded", "DepositCalcs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>("Amount", "Deposits");
            migrationBuilder.AlterColumn<double>("Percent", "Deposits");
            migrationBuilder.AlterColumn<double>("TotalAmount", "DepositCalcs");
            migrationBuilder.AlterColumn<double>("PercentAdded", "DepositCalcs");
        }
    }
}
