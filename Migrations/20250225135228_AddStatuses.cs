using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Summer_Camp_Status_Lookup",
                columns: new[] { "ID_Code", "Desc" },
                values: new object[,]
                {
                    { "F", "payment Failed by Parent" },
                    { "R", "Invitation Rejected by Parent" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "F");

            migrationBuilder.DeleteData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "R");
        }
    }
}
