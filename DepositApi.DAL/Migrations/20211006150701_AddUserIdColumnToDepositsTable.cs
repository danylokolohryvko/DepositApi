using Microsoft.EntityFrameworkCore.Migrations;

namespace DepositApi.DAL.Migrations
{
    public partial class AddUserIdColumnToDepositsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Deposits",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Deposits");
        }
    }
}
