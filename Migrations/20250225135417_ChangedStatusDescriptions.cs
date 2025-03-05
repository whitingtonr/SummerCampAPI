using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStatusDescriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "A",
                column: "Desc",
                value: "(A)pplied by Parent");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "C",
                column: "Desc",
                value: "A(C)cepted by Parent");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "F",
                column: "Desc",
                value: "Payment (F)ailed by Parent");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "I",
                column: "Desc",
                value: "(I)nvited - Seat Reserved");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "P",
                column: "Desc",
                value: "(P)aid by Parent");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "R",
                column: "Desc",
                value: "Invitation (R)ejected by Parent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "A",
                column: "Desc",
                value: "Applied by Parent");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "C",
                column: "Desc",
                value: "aCcepted by Parent");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "F",
                column: "Desc",
                value: "payment Failed by Parent");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "I",
                column: "Desc",
                value: "Invited - Seat Reserved");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "P",
                column: "Desc",
                value: "Paid by Parent");

            migrationBuilder.UpdateData(
                table: "Summer_Camp_Status_Lookup",
                keyColumn: "ID_Code",
                keyValue: "R",
                column: "Desc",
                value: "Invitation Rejected by Parent");
        }
    }
}
