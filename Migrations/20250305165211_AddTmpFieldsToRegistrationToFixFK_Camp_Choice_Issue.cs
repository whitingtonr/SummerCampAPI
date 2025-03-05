using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTmpFieldsToRegistrationToFixFK_Camp_Choice_Issue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TMP_SchoolName",
                table: "Summer_Camp_Registration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TMP_School_ID",
                table: "Summer_Camp_Registration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TMP_Summer_Camp_Title",
                table: "Summer_Camp_Registration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TMP_SchoolName",
                table: "Summer_Camp_Registration");

            migrationBuilder.DropColumn(
                name: "TMP_School_ID",
                table: "Summer_Camp_Registration");

            migrationBuilder.DropColumn(
                name: "TMP_Summer_Camp_Title",
                table: "Summer_Camp_Registration");
        }
    }
}
