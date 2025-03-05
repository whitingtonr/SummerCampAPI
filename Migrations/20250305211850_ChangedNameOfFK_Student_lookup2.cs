using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNameOfFK_Student_lookup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FK_Student_Registration__Student_ID",
                table: "Summer_Camp_Registration",
                newName: "FK_PCS_StudentLookup__Student_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FK_PCS_StudentLookup__Student_ID",
                table: "Summer_Camp_Registration",
                newName: "FK_Student_Registration__Student_ID");
        }
    }
}
