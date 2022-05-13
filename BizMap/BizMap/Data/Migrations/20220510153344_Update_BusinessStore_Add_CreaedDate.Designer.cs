﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skybrinns.Server.Data;

namespace BizMap.Data.Migrations
{
    [DbContext(typeof(BizDbContext))]
    [Migration("20220510153344_Update_BusinessStore_Add_CreaedDate")]
    partial class Update_BusinessStore_Add_CreaedDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BizMap.Models.BusinessStore", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BizCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<float>("Latitute")
                        .HasColumnType("float(10)");

                    b.Property<float>("Longitute")
                        .HasColumnType("float(10)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessUserId");

                    b.ToTable("BizStores");
                });

            modelBuilder.Entity("BizMap.Models.BusinessUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BizUsers");
                });

            modelBuilder.Entity("BizMap.Models.BusinessStore", b =>
                {
                    b.HasOne("BizMap.Models.BusinessUser", "BusinessUser")
                        .WithMany("BusinessStores")
                        .HasForeignKey("BusinessUserId");

                    b.Navigation("BusinessUser");
                });

            modelBuilder.Entity("BizMap.Models.BusinessUser", b =>
                {
                    b.Navigation("BusinessStores");
                });
#pragma warning restore 612, 618
        }
    }
}