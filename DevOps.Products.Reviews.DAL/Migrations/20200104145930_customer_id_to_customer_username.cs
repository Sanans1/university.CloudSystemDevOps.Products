using Microsoft.EntityFrameworkCore.Migrations;

namespace DevOps.Products.Reviews.DAL.Migrations
{
    public partial class customer_id_to_customer_username : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "CustomerUsername",
                table: "Reviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerUsername",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
