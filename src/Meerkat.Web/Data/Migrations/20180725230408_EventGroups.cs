using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meerkat.Web.Data.Migrations
{
    public partial class EventGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fingerprint",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fingerprint = table.Column<string>(nullable: true),
                    FirstSeenId = table.Column<long>(nullable: true),
                    LastSeenId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventGroups_Events_FirstSeenId",
                        column: x => x.FirstSeenId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventGroups_Events_LastSeenId",
                        column: x => x.LastSeenId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_GroupId",
                table: "Events",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroups_FirstSeenId",
                table: "EventGroups",
                column: "FirstSeenId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGroups_LastSeenId",
                table: "EventGroups",
                column: "LastSeenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventGroups_GroupId",
                table: "Events",
                column: "GroupId",
                principalTable: "EventGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventGroups_GroupId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventGroups");

            migrationBuilder.DropIndex(
                name: "IX_Events_GroupId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Fingerprint",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Events");
        }
    }
}
