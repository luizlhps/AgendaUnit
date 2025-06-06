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
    [Migration("20241224220339_alter_fields_sheduling")]
    partial class alter_fields_sheduling
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
                        .HasColumnType("int4")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bool")
                        .HasDefaultValue(true)
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int4")
                        .HasColumnName("owner_id");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamptz")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("TypeCompany")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type_company");

                    b.HasKey("Id")
                        .HasName("pk_company");

                    b.HasIndex("OwnerId")
                        .IsUnique()
                        .HasDatabaseName("ix_company_owner_id");

                    b.ToTable("company", "public");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int4")
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

                    b.Property<DateTimeOffset>("Timestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

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

                    b.Property<DateTimeOffset>("Timestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.ToTable("role");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Scheduling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int4")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CancelNote")
                        .HasColumnType("text")
                        .HasColumnName("cancel_note");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int4")
                        .HasColumnName("company_id");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int4")
                        .HasColumnName("customer_id");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamptz")
                        .HasColumnName("date");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision")
                        .HasColumnName("discount");

                    b.Property<TimeSpan>("Duration")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(6)
                        .HasColumnType("interval")
                        .HasColumnName("duration");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bool")
                        .HasDefaultValue(false)
                        .HasColumnName("isdeleted");

                    b.Property<string>("Notes")
                        .HasColumnType("text")
                        .HasColumnName("notes");

                    b.Property<int>("StaffUserId")
                        .HasColumnType("int4")
                        .HasColumnName("staff_user_id");

                    b.Property<int>("StatusId")
                        .HasColumnType("int4")
                        .HasColumnName("status_id");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamptz")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double precision")
                        .HasColumnName("total_price");

                    b.HasKey("Id")
                        .HasName("pk_scheduling");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_scheduling_company_id");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_scheduling_customer_id");

                    b.HasIndex("StaffUserId")
                        .HasDatabaseName("ix_scheduling_staff_user_id");

                    b.HasIndex("StatusId")
                        .IsUnique()
                        .HasDatabaseName("ix_scheduling_status_id");

                    b.ToTable("scheduling", "public");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.SchedulingService", b =>
                {
                    b.Property<int>("SchedulingId")
                        .HasColumnType("int4")
                        .HasColumnName("scheduling_id");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval")
                        .HasColumnName("duration");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bool")
                        .HasDefaultValue(true)
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.HasKey("SchedulingId", "ServiceId")
                        .HasName("pk_scheduling_service");

                    b.HasIndex("ServiceId")
                        .HasDatabaseName("ix_scheduling_service_service_id");

                    b.ToTable("scheduling_service", "public");
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
                        .HasColumnType("int4")
                        .HasColumnName("company_id");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("INTERVAL")
                        .HasColumnName("duration");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("isdeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer")
                        .HasColumnName("status_id");

                    b.Property<DateTimeOffset>("Timestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

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

                    b.Property<DateTimeOffset>("Timestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

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
                        .HasColumnType("int4")
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

                    b.Property<DateTimeOffset?>("RecoveryExpiryTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("recovery_expiry_time");

                    b.Property<string>("RecoveryToken")
                        .HasColumnType("text")
                        .HasColumnName("recovery_token");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<DateTimeOffset?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("refresh_token_expiry_time");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTimeOffset>("Timestamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

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
                        .WithMany("Schedulings")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_company_company_id");

                    b.HasOne("AgendaUnit.Domain.Models.Customer", "Customer")
                        .WithMany("Schedulings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_customer_customer_id");

                    b.HasOne("AgendaUnit.Domain.Models.User", "User")
                        .WithMany("Schedulings")
                        .HasForeignKey("StaffUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_user_staff_user_id");

                    b.HasOne("AgendaUnit.Domain.Models.Status", "Status")
                        .WithOne("Scheduling")
                        .HasForeignKey("AgendaUnit.Domain.Models.Scheduling", "StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_status_status_id");

                    b.Navigation("Company");

                    b.Navigation("Customer");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.SchedulingService", b =>
                {
                    b.HasOne("AgendaUnit.Domain.Models.Scheduling", "Scheduling")
                        .WithMany("SchedulingServices")
                        .HasForeignKey("SchedulingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_service_scheduling_scheduling_id");

                    b.HasOne("AgendaUnit.Domain.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_service_service_service_id");

                    b.Navigation("Scheduling");

                    b.Navigation("Service");
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

                    b.Navigation("Schedulings");

                    b.Navigation("Services");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Customer", b =>
                {
                    b.Navigation("Schedulings");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Scheduling", b =>
                {
                    b.Navigation("SchedulingServices");
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.Status", b =>
                {
                    b.Navigation("Scheduling")
                        .IsRequired();
                });

            modelBuilder.Entity("AgendaUnit.Domain.Models.User", b =>
                {
                    b.Navigation("Schedulings");
                });
#pragma warning restore 612, 618
        }
    }
}
