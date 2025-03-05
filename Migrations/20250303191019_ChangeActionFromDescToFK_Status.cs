using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeActionFromDescToFK_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewStatus",
                table: "Summer_Camp_Status_History",
                newName: "FK_NewStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FK_NewStatus",
                table: "Summer_Camp_Status_History",
                newName: "NewStatus");
        }
    }
}
