using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class fix_schedulingservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                schema: "public",
                table: "scheduling_service",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "timestamp",
                schema: "public",
                table: "scheduling_service",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id",
                schema: "public",
                table: "scheduling_service");

            migrationBuilder.DropColumn(
                name: "timestamp",
                schema: "public",
                table: "scheduling_service");
        }
    }
}
