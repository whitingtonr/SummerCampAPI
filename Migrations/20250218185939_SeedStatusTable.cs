using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SC_Status_Lookup",
                columns: new[] { "ID_Code", "Desc" },
                values: new object[,]
                {
                    { "A", "Applied by Parent" },
                    { "C", "aCcepted by Parent" },
                    { "I", "Invited - Seat Reserved" },
                    { "P", "Paid by Parent" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SC_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "A");

            migrationBuilder.DeleteData(
                table: "SC_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "C");

            migrationBuilder.DeleteData(
                table: "SC_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "I");

            migrationBuilder.DeleteData(
                table: "SC_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "P");
        }
    }
}
