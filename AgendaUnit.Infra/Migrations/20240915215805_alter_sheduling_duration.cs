using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class alter_sheduling_duration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hours",
                table: "scheduling");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "duration",
                table: "scheduling",
                type: "INTERVAL",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "scheduling");

            migrationBuilder.AddColumn<string>(
                name: "hours",
                table: "scheduling",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
