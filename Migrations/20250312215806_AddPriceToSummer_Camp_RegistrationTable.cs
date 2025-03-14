using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToSummer_Camp_RegistrationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Summer_Camp_Registration",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Summer_Camp_Registration");
        }
    }
}
