using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFBug.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyEntity",
                columns: table => new
                {
                    StartedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyEntity", x => new { x.StartedAt, x.Id });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyEntity");
        }
    }
}
