using Microsoft.EntityFrameworkCore.Migrations;

namespace BeekeepingDiary.Migrations
{
    public partial class InspectionAndProduceEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Produces",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Inspections",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Produces",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Inspections",
                newName: "Data");
        }
    }
}
