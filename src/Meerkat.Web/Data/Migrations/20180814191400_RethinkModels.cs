using Microsoft.EntityFrameworkCore.Migrations;

namespace Meerkat.Web.Data.Migrations
{
    public partial class RethinkModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Events",
                newName: "SdkVersion");

            migrationBuilder.RenameColumn(
                name: "Modules",
                table: "Events",
                newName: "Sdk");

            migrationBuilder.AddColumn<string>(
                name: "Module",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModuleVersion",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OSArchitecture",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Module",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ModuleVersion",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OSArchitecture",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SdkVersion",
                table: "Events",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Sdk",
                table: "Events",
                newName: "Modules");
        }
    }
}
