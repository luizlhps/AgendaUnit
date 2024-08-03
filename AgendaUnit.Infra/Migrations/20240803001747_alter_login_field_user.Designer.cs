﻿// <auto-generated />
using System;
using AgendaUnit.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240803001747_alter_login_field_user")]
    partial class alter_login_field_user
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AgendaUnit.Domain.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("owner_id");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<string>("TypeCompany")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type_company");

                    b.HasKey("Id")
                        .HasName("pk_company");

                    b.HasIndex("OwnerId")
                        .IsUnique()
                        .HasDatabaseName("ix_company_owner_id");

                    b.ToTable("company");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_customer_company_id");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.ToTable("role");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Scheduling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CancelNote")
                        .HasColumnType("text")
                        .HasColumnName("cancel_note");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("Hours")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hours");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("notes");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<int>("StaffUserId")
                        .HasColumnType("integer")
                        .HasColumnName("staff_user_id");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("total_price");

                    b.HasKey("Id")
                        .HasName("pk_scheduling");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_scheduling_company_id");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_scheduling_customer_id");

                    b.HasIndex("ServiceId")
                        .HasDatabaseName("ix_scheduling_service_id");

                    b.HasIndex("StaffUserId")
                        .HasDatabaseName("ix_scheduling_staff_user_id");

                    b.HasIndex("StatusId")
                        .HasDatabaseName("ix_scheduling_status_id");

                    b.ToTable("scheduling");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean")
                        .HasColumnName("ativo");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("duration");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id")
                        .HasName("pk_service");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_service_company_id");

                    b.HasIndex("StatusId")
                        .HasDatabaseName("ix_service_status_id");

                    b.ToTable("service");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id")
                        .HasName("pk_status");

                    b.ToTable("status");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("RecoveryToken")
                        .HasColumnType("text")
                        .HasColumnName("recovery_token");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_user_company_id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_user_email");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_role_id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ix_user_username");

                    b.ToTable("user");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Company", b =>
                {
                    b.HasOne("AgendaUnit.Domain.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_company_user_owner_id");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Customer", b =>
                {
                    b.HasOne("AgendaUnit.Domain.Models.Company", "Company")
                        .WithMany("Customers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customer_company_company_id");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Scheduling", b =>
                {
                    b.HasOne("AgendaUnit.Domain.Models.Company", "Company")
                        .WithMany("Scheduling")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_company_company_id");

                    b.HasOne("AgendaUnit.Domain.Models.Customer", "Customer")
                        .WithMany("Scheduling")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_customer_customer_id");

                    b.HasOne("AgendaUnit.Domain.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_service_service_id");

                    b.HasOne("AgendaUnit.Domain.Models.User", "StaffUser")
                        .WithMany()
                        .HasForeignKey("StaffUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_user_staff_user_id");

                    b.HasOne("AgendaUnit.Domain.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_status_status_id");

                    b.Navigation("Company");

                    b.Navigation("Customer");

                    b.Navigation("Service");

                    b.Navigation("StaffUser");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Service", b =>
                {
                    b.HasOne("AgendaUnit.Domain.Models.Company", "Company")
                        .WithMany("Services")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_service_company_company_id");

                    b.HasOne("AgendaUnit.Domain.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_service_status_status_id");

                    b.Navigation("Company");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.User", b =>
                {
                    b.HasOne("AgendaUnit.Domain.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("fk_user_company_company_id");

                    b.HasOne("AgendaUnit.Domain.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_role_id");

                    b.Navigation("Company");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Company", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Scheduling");

                    b.Navigation("Services");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Customer", b =>
                {
                    b.Navigation("Scheduling");
                });
#pragma warning restore 612, 618
        }
    }
}
