using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTMP_AndAddedChoiceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FK_Summer_Camps",
                table: "Summer_Camp_Registration");

            migrationBuilder.RenameColumn(
                name: "TMP_Summer_Camp_Title",
                table: "Summer_Camp_Registration",
                newName: "UpdateUser");

            migrationBuilder.RenameColumn(
                name: "TMP_School_ID",
                table: "Summer_Camp_Registration",
                newName: "Summer_Camp_Title");

            migrationBuilder.RenameColumn(
                name: "TMP_SchoolName",
                table: "Summer_Camp_Registration",
                newName: "School_ID");

            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                table: "Summer_Camp_Registration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Summer_Camp_Registration",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolName",
                table: "Summer_Camp_Registration");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Summer_Camp_Registration");

            migrationBuilder.RenameColumn(
                name: "UpdateUser",
                table: "Summer_Camp_Registration",
                newName: "TMP_Summer_Camp_Title");

            migrationBuilder.RenameColumn(
                name: "Summer_Camp_Title",
                table: "Summer_Camp_Registration",
                newName: "TMP_School_ID");

            migrationBuilder.RenameColumn(
                name: "School_ID",
                table: "Summer_Camp_Registration",
                newName: "TMP_SchoolName");

            migrationBuilder.AddColumn<int>(
                name: "FK_Summer_Camps",
                table: "Summer_Camp_Registration",
                type: "int",
                nullable: true);
        }
    }
}
