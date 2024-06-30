using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class fix_guid_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Para converter cada coluna integer para uuid, você deve especificar a conversão
            migrationBuilder.Sql("ALTER TABLE \"user\" ALTER COLUMN \"uuid\" TYPE uuid USING uuid::text::uuid;");
            migrationBuilder.Sql("ALTER TABLE \"service\" ALTER COLUMN \"uuid\" TYPE uuid USING uuid::text::uuid;");
            migrationBuilder.Sql("ALTER TABLE \"scheduling\" ALTER COLUMN \"uuid\" TYPE uuid USING uuid::text::uuid;");
            migrationBuilder.Sql("ALTER TABLE \"customer\" ALTER COLUMN \"uuid\" TYPE uuid USING uuid::text::uuid;");
            migrationBuilder.Sql("ALTER TABLE \"company\" ALTER COLUMN \"uuid\" TYPE uuid USING uuid::text::uuid;");
            migrationBuilder.Sql("ALTER TABLE \"businesshour\" ALTER COLUMN \"uuid\" TYPE uuid USING uuid::text::uuid;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertendo para o tipo integer, se necessário
            migrationBuilder.Sql("ALTER TABLE \"user\" ALTER COLUMN \"uuid\" TYPE integer USING uuid::text::integer;");
            migrationBuilder.Sql("ALTER TABLE \"service\" ALTER COLUMN \"uuid\" TYPE integer USING uuid::text::integer;");
            migrationBuilder.Sql("ALTER TABLE \"scheduling\" ALTER COLUMN \"uuid\" TYPE integer USING uuid::text::integer;");
            migrationBuilder.Sql("ALTER TABLE \"customer\" ALTER COLUMN \"uuid\" TYPE integer USING uuid::text::integer;");
            migrationBuilder.Sql("ALTER TABLE \"company\" ALTER COLUMN \"uuid\" TYPE integer USING uuid::text::integer;");
            migrationBuilder.Sql("ALTER TABLE \"businesshour\" ALTER COLUMN \"uuid\" TYPE integer USING uuid::text::integer;");
        }
    }
}
