﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class alter_fields_sheduling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    type_company = table.Column<string>(type: "text", nullable: false),
                    owner_id = table.Column<int>(type: "int4", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    isdeleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    company_id = table.Column<int>(type: "int4", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                    table.ForeignKey(
                        name: "fk_customer_company_company_id",
                        column: x => x.company_id,
                        principalSchema: "public",
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<TimeSpan>(type: "INTERVAL", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    company_id = table.Column<int>(type: "int4", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service", x => x.id);
                    table.ForeignKey(
                        name: "fk_service_company_company_id",
                        column: x => x.company_id,
                        principalSchema: "public",
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_service_status_status_id",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    recovery_token = table.Column<string>(type: "text", nullable: true),
                    recovery_expiry_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    refresh_token = table.Column<string>(type: "text", nullable: true),
                    refresh_token_expiry_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: false),
                    company_id = table.Column<int>(type: "int4", nullable: true),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_company_company_id",
                        column: x => x.company_id,
                        principalSchema: "public",
                        principalTable: "company",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scheduling",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status_id = table.Column<int>(type: "int4", nullable: false),
                    staff_user_id = table.Column<int>(type: "int4", nullable: false),
                    company_id = table.Column<int>(type: "int4", nullable: false),
                    customer_id = table.Column<int>(type: "int4", nullable: false),
                    date = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    duration = table.Column<TimeSpan>(type: "interval(6)", precision: 6, nullable: false),
                    cancel_note = table.Column<string>(type: "text", nullable: true),
                    total_price = table.Column<double>(type: "double precision", nullable: false),
                    discount = table.Column<double>(type: "double precision", nullable: false),
                    isdeleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scheduling", x => x.id);
                    table.ForeignKey(
                        name: "fk_scheduling_company_company_id",
                        column: x => x.company_id,
                        principalSchema: "public",
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_scheduling_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_scheduling_status_status_id",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_scheduling_user_staff_user_id",
                        column: x => x.staff_user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scheduling_service",
                schema: "public",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    scheduling_id = table.Column<int>(type: "int4", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    isdeleted = table.Column<bool>(type: "bool", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scheduling_service", x => new { x.scheduling_id, x.service_id });
                    table.ForeignKey(
                        name: "fk_scheduling_service_scheduling_scheduling_id",
                        column: x => x.scheduling_id,
                        principalSchema: "public",
                        principalTable: "scheduling",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_scheduling_service_service_service_id",
                        column: x => x.service_id,
                        principalTable: "service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_company_owner_id",
                schema: "public",
                table: "company",
                column: "owner_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customer_company_id",
                table: "customer",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_company_id",
                schema: "public",
                table: "scheduling",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_customer_id",
                schema: "public",
                table: "scheduling",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_staff_user_id",
                schema: "public",
                table: "scheduling",
                column: "staff_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_status_id",
                schema: "public",
                table: "scheduling",
                column: "status_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scheduling_service_service_id",
                schema: "public",
                table: "scheduling_service",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "ix_service_company_id",
                table: "service",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_service_status_id",
                table: "service",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_company_id",
                table: "user",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_username",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_company_user_owner_id",
                schema: "public",
                table: "company",
                column: "owner_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_user_owner_id",
                schema: "public",
                table: "company");

            migrationBuilder.DropTable(
                name: "scheduling_service",
                schema: "public");

            migrationBuilder.DropTable(
                name: "scheduling",
                schema: "public");

            migrationBuilder.DropTable(
                name: "service");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "company",
                schema: "public");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
