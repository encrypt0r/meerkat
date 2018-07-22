using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meerkat.Web.Data.Migrations
{
    public partial class Events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Release = table.Column<string>(nullable: true),
                    RootCause = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    OperatingSystem = table.Column<string>(nullable: true),
                    Runtime = table.Column<string>(nullable: true),
                    Modules = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Function = table.Column<string>(nullable: true),
                    Module = table.Column<string>(nullable: true),
                    ContextLine = table.Column<string>(nullable: true),
                    InApp = table.Column<bool>(nullable: false),
                    LineNumber = table.Column<int>(nullable: false),
                    ColumnNumber = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    EventId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frames_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frames_EventId",
                table: "Frames",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
