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
    [Migration("20240623171820_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AgendaUnit.Domain.models.BusinessHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ClosingTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("closingtime");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("companyid");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("dayofweek");

                    b.Property<DateTime>("OpeningTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("openingtime");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<int>("Uuid")
                        .HasColumnType("integer")
                        .HasColumnName("uuid");

                    b.HasKey("Id")
                        .HasName("pk_businesshour");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_businesshour_companyid");

                    b.ToTable("businesshour");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("ownerid");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<string>("TypeCompany")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("typecompany");

                    b.Property<int>("Uuid")
                        .HasColumnType("integer")
                        .HasColumnName("uuid");

                    b.HasKey("Id")
                        .HasName("pk_company");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_company_ownerid");

                    b.ToTable("company");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("companyid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<int>("Uuid")
                        .HasColumnType("integer")
                        .HasColumnName("uuid");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Scheduling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CancelNote")
                        .HasColumnType("text")
                        .HasColumnName("cancelnote");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("companyid");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customerid");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("Hours")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hours");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("notes");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("serviceid");

                    b.Property<int>("StaffUserId")
                        .HasColumnType("integer")
                        .HasColumnName("staffuserid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("totalprice");

                    b.Property<int>("Uuid")
                        .HasColumnType("integer")
                        .HasColumnName("uuid");

                    b.HasKey("Id")
                        .HasName("pk_scheduling");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_scheduling_companyid");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_scheduling_customerid");

                    b.HasIndex("ServiceId")
                        .HasDatabaseName("ix_scheduling_serviceid");

                    b.HasIndex("StaffUserId")
                        .HasDatabaseName("ix_scheduling_staffuserid");

                    b.ToTable("scheduling");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("companyid");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("duration");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<int>("Uuid")
                        .HasColumnType("integer")
                        .HasColumnName("uuid");

                    b.HasKey("Id")
                        .HasName("pk_service");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_service_companyid");

                    b.ToTable("service");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthdate");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("companyid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

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
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("recoverytoken");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<int>("Uuid")
                        .HasColumnType("integer")
                        .HasColumnName("uuid");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_user_companyid");

                    b.ToTable("user");
                });

            modelBuilder.Entity("CompanyCustomer", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("companyid");

                    b.Property<int>("CustomersId")
                        .HasColumnType("integer")
                        .HasColumnName("customersid");

                    b.HasKey("CompanyId", "CustomersId")
                        .HasName("pk_companycustomer");

                    b.HasIndex("CustomersId")
                        .HasDatabaseName("ix_companycustomer_customersid");

                    b.ToTable("companycustomer");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.BusinessHours", b =>
                {
                    b.HasOne("AgendaUnit.Domain.models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_businesshour_company_companyid");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Company", b =>
                {
                    b.HasOne("AgendaUnit.Domain.models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_company_user_ownerid");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Scheduling", b =>
                {
                    b.HasOne("AgendaUnit.Domain.models.Company", "Company")
                        .WithMany("Scheduling")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_company_companyid");

                    b.HasOne("AgendaUnit.Domain.models.Customer", "Customer")
                        .WithMany("Scheduling")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_customer_customerid");

                    b.HasOne("AgendaUnit.Domain.models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_service_serviceid");

                    b.HasOne("AgendaUnit.Domain.models.User", "StaffUser")
                        .WithMany()
                        .HasForeignKey("StaffUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_scheduling_user_staffuserid");

                    b.Navigation("Company");

                    b.Navigation("Customer");

                    b.Navigation("Service");

                    b.Navigation("StaffUser");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Service", b =>
                {
                    b.HasOne("AgendaUnit.Domain.models.Company", "Company")
                        .WithMany("Services")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_service_company_companyid");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.User", b =>
                {
                    b.HasOne("AgendaUnit.Domain.models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("fk_user_company_companyid");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CompanyCustomer", b =>
                {
                    b.HasOne("AgendaUnit.Domain.models.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_companycustomer_company_companyid");

                    b.HasOne("AgendaUnit.Domain.models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_companycustomer_customer_customersid");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Company", b =>
                {
                    b.Navigation("Scheduling");

                    b.Navigation("Services");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AgendaUnit.Domain.models.Customer", b =>
                {
                    b.Navigation("Scheduling");
                });
#pragma warning restore 612, 618
        }
    }
}
