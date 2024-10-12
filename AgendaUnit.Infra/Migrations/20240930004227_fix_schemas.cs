using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class fix_schemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_scheduling_status_id",
                table: "scheduling");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "scheduling",
                newName: "scheduling",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "company",
                newName: "company",
                newSchema: "public");

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "user",
                type: "int4",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "service",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "customer",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "timestamp",
                schema: "public",
                table: "scheduling",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "status_id",
                schema: "public",
                table: "scheduling",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "staff_user_id",
                schema: "public",
                table: "scheduling",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "service_id",
                schema: "public",
                table: "scheduling",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                schema: "public",
                table: "scheduling",
                type: "bool",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "duration",
                schema: "public",
                table: "scheduling",
                type: "interval(6)",
                precision: 6,
                nullable: false,
                defaultValueSql: "'00:00:00'",
                oldClrType: typeof(TimeSpan),
                oldType: "INTERVAL");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                schema: "public",
                table: "scheduling",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                schema: "public",
                table: "scheduling",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                schema: "public",
                table: "scheduling",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "cancel_note",
                schema: "public",
                table: "scheduling",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "public",
                table: "scheduling",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "timestamp",
                schema: "public",
                table: "company",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "owner_id",
                schema: "public",
                table: "company",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                schema: "public",
                table: "company",
                type: "bool",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                schema: "public",
                table: "company",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_status_id",
                schema: "public",
                table: "scheduling",
                column: "status_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_scheduling_status_id",
                schema: "public",
                table: "scheduling");

            migrationBuilder.RenameTable(
                name: "scheduling",
                schema: "public",
                newName: "scheduling");

            migrationBuilder.RenameTable(
                name: "company",
                schema: "public",
                newName: "company");

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "user",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int4",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "service",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "customer",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "timestamp",
                table: "scheduling",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<int>(
                name: "status_id",
                table: "scheduling",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<int>(
                name: "staff_user_id",
                table: "scheduling",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<int>(
                name: "service_id",
                table: "scheduling",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "scheduling",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bool");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "duration",
                table: "scheduling",
                type: "INTERVAL",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval(6)",
                oldPrecision: 6,
                oldDefaultValueSql: "'00:00:00'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "scheduling",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<int>(
                name: "customer_id",
                table: "scheduling",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<int>(
                name: "company_id",
                table: "scheduling",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<string>(
                name: "cancel_note",
                table: "scheduling",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "scheduling",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "timestamp",
                table: "company",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<int>(
                name: "owner_id",
                table: "company",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "company",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bool");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "company",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_status_id",
                table: "scheduling",
                column: "status_id");
        }
    }
}
