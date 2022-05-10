using Microsoft.EntityFrameworkCore.Migrations;

namespace BizMap.Data.Migrations
{
    public partial class Upate_BusineStore_Add_Country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Longitute",
                table: "BizStores",
                type: "float(10)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Latitute",
                table: "BizStores",
                type: "float(10)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "BizStores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "BizStores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "BizStores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "BizStores",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "BizStores");

            migrationBuilder.DropColumn(
                name: "City",
                table: "BizStores");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "BizStores");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BizStores");

            migrationBuilder.AlterColumn<float>(
                name: "Longitute",
                table: "BizStores",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float(10)");

            migrationBuilder.AlterColumn<float>(
                name: "Latitute",
                table: "BizStores",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float(10)");
        }
    }
}
