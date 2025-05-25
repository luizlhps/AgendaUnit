using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class alter_schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM scheduling_service");

            migrationBuilder.DropPrimaryKey(
                name: "pk_scheduling_service",
                schema: "public",
                table: "scheduling_service");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "public",
                table: "scheduling_service",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_scheduling_service",
                schema: "public",
                table: "scheduling_service",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_service_scheduling_id",
                schema: "public",
                table: "scheduling_service",
                column: "scheduling_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_scheduling_service",
                schema: "public",
                table: "scheduling_service");

            migrationBuilder.DropIndex(
                name: "ix_scheduling_service_scheduling_id",
                schema: "public",
                table: "scheduling_service");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "public",
                table: "scheduling_service",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_scheduling_service",
                schema: "public",
                table: "scheduling_service",
                columns: new[] { "scheduling_id", "service_id" });
        }
    }
}
