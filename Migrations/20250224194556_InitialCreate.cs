using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SummerCampAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Summer_Camp_Payments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Summer_Camp_Registration = table.Column<int>(type: "int", nullable: false),
                    Event_Type = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    Message = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summer_Camp_Payments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Summer_Camp_Registration",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Student_Registration__Student_ID = table.Column<int>(type: "int", nullable: false),
                    FK_Summer_Camps = table.Column<int>(type: "int", nullable: false),
                    FK_Summer_Camp_Choice = table.Column<int>(type: "int", nullable: false),
                    FK_Status = table.Column<int>(type: "int", nullable: false),
                    CalendarYR = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summer_Camp_Registration", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Summer_Camp_Status_History",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Summer_Camp_Registration = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summer_Camp_Status_History", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Summer_Camp_Status_Lookup",
                columns: table => new
                {
                    ID_Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Desc = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summer_Camp_Status_Lookup", x => x.ID_Code);
                });

            migrationBuilder.InsertData(
                table: "Summer_Camp_Status_Lookup",
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
            migrationBuilder.DropTable(
                name: "Summer_Camp_Payments");

            migrationBuilder.DropTable(
                name: "Summer_Camp_Registration");

            migrationBuilder.DropTable(
                name: "Summer_Camp_Status_History");

            migrationBuilder.DropTable(
                name: "Summer_Camp_Status_Lookup");
        }
    }
}
