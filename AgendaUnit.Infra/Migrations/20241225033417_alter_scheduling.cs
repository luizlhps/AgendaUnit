using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class alter_scheduling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_scheduling_status_id",
                schema: "public",
                table: "scheduling");

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_status_id",
                schema: "public",
                table: "scheduling",
                column: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_scheduling_status_id",
                schema: "public",
                table: "scheduling");

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_status_id",
                schema: "public",
                table: "scheduling",
                column: "status_id",
                unique: true);
        }
    }
}
