﻿// <auto-generated />
using System;
using HuoltoWebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HuoltoWebApp.Migrations
{
    [DbContext(typeof(HuoltoContext))]
    [Migration("20240515082350_UpdateHuoltoIdAsIdentityHuoltopyyntoSailio")]
    partial class UpdateHuoltoIdAsIdentityHuoltopyyntoSailio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HuoltoWebApp.Areas.Identity.Data.HuoltoWebAppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Auto", b =>
                {
                    b.Property<int>("AutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AutoID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AutoId"), 1L, 1);

                    b.Property<DateTime?>("Adr")
                        .HasColumnType("date")
                        .HasColumnName("ADR");

                    b.Property<DateTime?>("Alkolukko")
                        .HasColumnType("date");

                    b.Property<int?>("AutoInfoId")
                        .HasColumnType("int")
                        .HasColumnName("AutoInfoID");

                    b.Property<DateTime?>("Katsastus")
                        .HasColumnType("date");

                    b.Property<DateTime?>("Nopeudenrajoitin")
                        .HasColumnType("date");

                    b.Property<DateTime?>("Piirturi")
                        .HasColumnType("date");

                    b.Property<string>("RekNro")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("REK-NRO");

                    b.Property<int?>("SäiliöId")
                        .HasColumnType("int")
                        .HasColumnName("SäiliöID");

                    b.HasKey("AutoId");

                    b.HasIndex("SäiliöId");

                    b.ToTable("Auto", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoHuollot", b =>
                {
                    b.Property<int>("HuollonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("HuollonID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HuollonId"), 1L, 1);

                    b.Property<int>("AutoId")
                        .HasColumnType("int")
                        .HasColumnName("AutoID");

                    b.Property<string>("HuollonKuvaus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("HuoltoPvm")
                        .HasColumnType("datetime");

                    b.Property<int?>("HuoltopaikkaId")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("HuoltopaikkaID");

                    b.Property<byte[]>("Kuva")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("HuollonId")
                        .HasName("PK__AutoHuol__5FD7B1EA7BC416AB");

                    b.HasIndex("AutoId");

                    b.ToTable("AutoHuollot", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoHuoltopyyntö", b =>
                {
                    b.Property<int>("HuoltoId")
                        .HasColumnType("int")
                        .HasColumnName("HuoltoID");

                    b.Property<int>("AutoId")
                        .HasColumnType("int")
                        .HasColumnName("AutoID");

                    b.Property<string>("HuollonKuvaus")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<byte[]>("Kuva")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("HuoltoId")
                        .HasName("PK__AutoHuol__BE746FF02A295F37");

                    b.HasIndex("AutoId");

                    b.ToTable("AutoHuoltopyyntö", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoInfo", b =>
                {
                    b.Property<int>("AutoInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AutoInfoID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AutoInfoId"), 1L, 1);

                    b.Property<int>("AutoId")
                        .HasColumnType("int")
                        .HasColumnName("AutoID");

                    b.Property<string>("InfoTxt")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("InfoTXT");

                    b.HasKey("AutoInfoId");

                    b.HasIndex(new[] { "AutoId" }, "UQ__AutoInfo__6B232964F93818B5")
                        .IsUnique();

                    b.ToTable("AutoInfo", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoMuistutu", b =>
                {
                    b.Property<int>("AutoMuistutusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AutoMuistutusID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AutoMuistutusId"), 1L, 1);

                    b.Property<int?>("AutoId")
                        .HasColumnType("int")
                        .HasColumnName("AutoID");

                    b.Property<DateTime?>("MuistutusPvm")
                        .HasColumnType("date")
                        .HasColumnName("MuistutusPVM");

                    b.Property<int?>("Muistutustyyppi")
                        .HasColumnType("int");

                    b.HasKey("AutoMuistutusId")
                        .HasName("PK__AutoMuis__37A01D1C38893DA6");

                    b.HasIndex("AutoId");

                    b.HasIndex("Muistutustyyppi");

                    b.ToTable("AutoMuistutus");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Huoltopaikat", b =>
                {
                    b.Property<int>("HuoltoPaikkaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("HuoltoPaikkaID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HuoltoPaikkaId"), 1L, 1);

                    b.Property<string>("Huoltopaikka")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("HuoltoPaikkaId")
                        .HasName("PK__Huoltopa__DADB2282F9D2430F");

                    b.ToTable("Huoltopaikat", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Kuva", b =>
                {
                    b.Property<int>("KuvaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("KuvaID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KuvaId"), 1L, 1);

                    b.Property<int>("AutoInfoId")
                        .HasColumnType("int");

                    b.Property<byte[]>("KuvaData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("KuvaNimi")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("KuvaId");

                    b.HasIndex("AutoInfoId");

                    b.ToTable("Kuva", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Muistutustyyppi", b =>
                {
                    b.Property<int>("MuistutustyyppiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MuistutustyyppiID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MuistutustyyppiId"), 1L, 1);

                    b.Property<string>("MuistutustyyppiNimi")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MuistutustyyppiId");

                    b.ToTable("Muistutustyyppi", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Pv", b =>
                {
                    b.Property<int>("PvId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PvID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PvId"), 1L, 1);

                    b.Property<DateTime?>("Adr")
                        .HasColumnType("date")
                        .HasColumnName("ADR");

                    b.Property<DateTime?>("Katsastus")
                        .HasColumnType("date");

                    b.Property<DateTime?>("Määräaikaistarkastus")
                        .HasColumnType("date");

                    b.Property<int?>("PvInfoId")
                        .HasColumnType("int")
                        .HasColumnName("PvInfoID");

                    b.Property<string>("RekNro")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("REK-NRO");

                    b.Property<DateTime?>("Välitarkastus")
                        .HasColumnType("date");

                    b.HasKey("PvId");

                    b.ToTable("PV", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.PvHuollot", b =>
                {
                    b.Property<int>("HuoltoId")
                        .HasColumnType("int")
                        .HasColumnName("HuoltoID");

                    b.Property<string>("HuollonKuvaus")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("HuoltoPaikkaId")
                        .HasColumnType("int")
                        .HasColumnName("HuoltoPaikkaID");

                    b.Property<DateTime?>("HuoltoPvm")
                        .HasColumnType("date");

                    b.Property<byte[]>("Kuva")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("PvId")
                        .HasColumnType("int")
                        .HasColumnName("PvID");

                    b.HasKey("HuoltoId")
                        .HasName("PK__PvHuollo__BE746FF09B0C99D1");

                    b.HasIndex("PvId");

                    b.ToTable("PvHuollot", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.PvHuoltopyyntö", b =>
                {
                    b.Property<int>("HuoltoId")
                        .HasColumnType("int")
                        .HasColumnName("HuoltoID");

                    b.Property<string>("HuollonKuvaus")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<byte[]>("Kuva")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("PvId")
                        .HasColumnType("int")
                        .HasColumnName("PvID");

                    b.HasKey("HuoltoId")
                        .HasName("PK__PvHuolto__BE746FF0B0DE434F");

                    b.HasIndex("PvId");

                    b.ToTable("PvHuoltopyyntö", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.PvInfo", b =>
                {
                    b.Property<int>("PvInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PvInfoID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PvInfoId"), 1L, 1);

                    b.Property<string>("InfoTxt")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("InfoTXT");

                    b.Property<byte[]>("Kuva")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("PvId")
                        .HasColumnType("int")
                        .HasColumnName("PvID");

                    b.HasKey("PvInfoId");

                    b.HasIndex(new[] { "PvId" }, "UQ__PvInfo__9A008323337D0F32")
                        .IsUnique();

                    b.ToTable("PvInfo", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.PvMuistutu", b =>
                {
                    b.Property<int>("PvMuistutusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PvMuistutusID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PvMuistutusId"), 1L, 1);

                    b.Property<DateTime?>("MuistutusPvm")
                        .HasColumnType("date")
                        .HasColumnName("MuistutusPVM");

                    b.Property<int?>("Muistutustyyppi")
                        .HasColumnType("int");

                    b.Property<int?>("PvId")
                        .HasColumnType("int")
                        .HasColumnName("PvID");

                    b.HasKey("PvMuistutusId")
                        .HasName("PK__PvMuistu__1BD388FDD942EBB5");

                    b.HasIndex("Muistutustyyppi");

                    b.HasIndex("PvId");

                    b.ToTable("PvMuistutus");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Säiliö", b =>
                {
                    b.Property<int>("SäiliöId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SäiliöID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SäiliöId"), 1L, 1);

                    b.Property<DateTime?>("Määräaikaistarkastus")
                        .HasColumnType("date");

                    b.Property<int?>("SäiliöInfoId")
                        .HasColumnType("int")
                        .HasColumnName("SäiliöInfoID");

                    b.Property<int>("SäiliöNro")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Vakaus")
                        .HasColumnType("date");

                    b.Property<DateTime?>("Välitarkastus")
                        .HasColumnType("date");

                    b.HasKey("SäiliöId");

                    b.ToTable("Säiliö", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.SäiliöHuollot", b =>
                {
                    b.Property<int>("HuoltoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("HuoltoID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HuoltoId"), 1L, 1);

                    b.Property<string>("HuollonKuvaus")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("HuoltoPaikkaId")
                        .HasColumnType("int")
                        .HasColumnName("HuoltoPaikkaID");

                    b.Property<DateTime?>("HuoltoPvm")
                        .HasColumnType("date");

                    b.Property<byte[]>("Kuva")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("SäiliöId")
                        .HasColumnType("int")
                        .HasColumnName("SäiliöID");

                    b.HasKey("HuoltoId")
                        .HasName("PK__SäiliöHu__BE746FF0346755C9");

                    b.HasIndex("SäiliöId");

                    b.ToTable("SäiliöHuollot", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.SäiliöHuoltopyyntö", b =>
                {
                    b.Property<int>("HuoltoId")
                        .HasColumnType("int")
                        .HasColumnName("HuoltoID");

                    b.Property<string>("HuollonKuvaus")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<byte[]>("Kuva")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("SäiliöId")
                        .HasColumnType("int")
                        .HasColumnName("SäiliöID");

                    b.HasKey("HuoltoId")
                        .HasName("PK__SäiliöHu__BE746FF078774EDD");

                    b.HasIndex("SäiliöId");

                    b.ToTable("SäiliöHuoltopyyntö", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.SäiliöInfo", b =>
                {
                    b.Property<int>("SäiliöInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SäiliöInfoID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SäiliöInfoId"), 1L, 1);

                    b.Property<string>("InfoTxt")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("InfoTXT");

                    b.Property<byte[]>("Kuva")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("SäiliöId")
                        .HasColumnType("int")
                        .HasColumnName("SäiliöID");

                    b.HasKey("SäiliöInfoId");

                    b.HasIndex(new[] { "SäiliöId" }, "UQ__SäiliöIn__21300E71C1C62E57")
                        .IsUnique();

                    b.ToTable("SäiliöInfo", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.SäiliöMuistutu", b =>
                {
                    b.Property<int>("SäiliöMuistutusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SäiliöMuistutusID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SäiliöMuistutusId"), 1L, 1);

                    b.Property<DateTime?>("MuistutusPvm")
                        .HasColumnType("date")
                        .HasColumnName("MuistutusPVM");

                    b.Property<int?>("Muistutustyyppi")
                        .HasColumnType("int");

                    b.Property<int?>("SäiliöId")
                        .HasColumnType("int")
                        .HasColumnName("SäiliöID");

                    b.HasKey("SäiliöMuistutusId")
                        .HasName("PK__SäiliöMu__41E798D7F1E7485D");

                    b.HasIndex("Muistutustyyppi");

                    b.HasIndex("SäiliöId");

                    b.ToTable("SäiliöMuistutus");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Auto", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Säiliö", "Säiliö")
                        .WithMany("Autos")
                        .HasForeignKey("SäiliöId")
                        .HasConstraintName("FK_Auto_Säiliö");

                    b.Navigation("Säiliö");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoHuollot", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Auto", "Auto")
                        .WithMany("AutoHuollots")
                        .HasForeignKey("AutoId")
                        .IsRequired()
                        .HasConstraintName("FK_AutoHuollot_Auto");

                    b.Navigation("Auto");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoHuoltopyyntö", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Auto", "Auto")
                        .WithMany("AutoHuoltopyyntös")
                        .HasForeignKey("AutoId")
                        .IsRequired()
                        .HasConstraintName("FK_AutoHuoltopyyntö_Auto");

                    b.Navigation("Auto");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoInfo", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Auto", "Auto")
                        .WithOne("AutoInfo")
                        .HasForeignKey("HuoltoWebApp.Models.AutoInfo", "AutoId")
                        .IsRequired()
                        .HasConstraintName("FK_AutoInfo_Auto");

                    b.Navigation("Auto");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoMuistutu", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Auto", "Auto")
                        .WithMany("AutoMuistutus")
                        .HasForeignKey("AutoId")
                        .HasConstraintName("FK_AutoMuistutus_Auto");

                    b.HasOne("HuoltoWebApp.Models.Muistutustyyppi", "MuistutustyyppiNavigation")
                        .WithMany("AutoMuistutus")
                        .HasForeignKey("Muistutustyyppi")
                        .HasConstraintName("FK_AutoMuistutus_Muistutustyyppi");

                    b.Navigation("Auto");

                    b.Navigation("MuistutustyyppiNavigation");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Kuva", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.AutoInfo", "AutoInfo")
                        .WithMany("Kuvat")
                        .HasForeignKey("AutoInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Kuva_AutoInfo");

                    b.Navigation("AutoInfo");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.PvHuollot", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Pv", "Pv")
                        .WithMany("PvHuollots")
                        .HasForeignKey("PvId")
                        .IsRequired()
                        .HasConstraintName("FK_PvHuollot_Pv");

                    b.Navigation("Pv");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.PvHuoltopyyntö", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Pv", "Pv")
                        .WithMany("PvHuoltopyyntös")
                        .HasForeignKey("PvId")
                        .IsRequired()
                        .HasConstraintName("FK_PvHuoltopyyntö_PV");

                    b.Navigation("Pv");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.PvInfo", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Pv", "Pv")
                        .WithOne("PvInfo")
                        .HasForeignKey("HuoltoWebApp.Models.PvInfo", "PvId")
                        .IsRequired()
                        .HasConstraintName("FK_PvInfo_Pv");

                    b.Navigation("Pv");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.PvMuistutu", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Muistutustyyppi", "MuistutustyyppiNavigation")
                        .WithMany("PvMuistutus")
                        .HasForeignKey("Muistutustyyppi")
                        .HasConstraintName("FK_PvMuistutus_Muistutustyyppi");

                    b.HasOne("HuoltoWebApp.Models.Pv", "Pv")
                        .WithMany("PvMuistutus")
                        .HasForeignKey("PvId")
                        .HasConstraintName("FK_PvMuistutus_PV");

                    b.Navigation("MuistutustyyppiNavigation");

                    b.Navigation("Pv");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.SäiliöHuollot", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Säiliö", "Säiliö")
                        .WithMany("SäiliöHuollots")
                        .HasForeignKey("SäiliöId")
                        .IsRequired()
                        .HasConstraintName("FK_SäiliöHuollot_Säiliö");

                    b.Navigation("Säiliö");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.SäiliöHuoltopyyntö", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Säiliö", "Säiliö")
                        .WithMany("SäiliöHuoltopyyntös")
                        .HasForeignKey("SäiliöId")
                        .IsRequired()
                        .HasConstraintName("FK_SäiliöHuoltopyyntö_Säiliö");

                    b.Navigation("Säiliö");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.SäiliöInfo", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Säiliö", "Säiliö")
                        .WithOne("SäiliöInfo")
                        .HasForeignKey("HuoltoWebApp.Models.SäiliöInfo", "SäiliöId")
                        .IsRequired()
                        .HasConstraintName("FK_SäiliöInfo_Säiliö");

                    b.Navigation("Säiliö");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.SäiliöMuistutu", b =>
                {
                    b.HasOne("HuoltoWebApp.Models.Muistutustyyppi", "MuistutustyyppiNavigation")
                        .WithMany("SäiliöMuistutus")
                        .HasForeignKey("Muistutustyyppi")
                        .HasConstraintName("FK_SäiliöMuistutus_Muistutustyyppi");

                    b.HasOne("HuoltoWebApp.Models.Säiliö", "Säiliö")
                        .WithMany("SäiliöMuistutus")
                        .HasForeignKey("SäiliöId")
                        .HasConstraintName("FK_SäiliöMuistutus_Säiliö");

                    b.Navigation("MuistutustyyppiNavigation");

                    b.Navigation("Säiliö");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HuoltoWebApp.Areas.Identity.Data.HuoltoWebAppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HuoltoWebApp.Areas.Identity.Data.HuoltoWebAppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HuoltoWebApp.Areas.Identity.Data.HuoltoWebAppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HuoltoWebApp.Areas.Identity.Data.HuoltoWebAppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Auto", b =>
                {
                    b.Navigation("AutoHuollots");

                    b.Navigation("AutoHuoltopyyntös");

                    b.Navigation("AutoInfo");

                    b.Navigation("AutoMuistutus");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.AutoInfo", b =>
                {
                    b.Navigation("Kuvat");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Muistutustyyppi", b =>
                {
                    b.Navigation("AutoMuistutus");

                    b.Navigation("PvMuistutus");

                    b.Navigation("SäiliöMuistutus");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Pv", b =>
                {
                    b.Navigation("PvHuollots");

                    b.Navigation("PvHuoltopyyntös");

                    b.Navigation("PvInfo");

                    b.Navigation("PvMuistutus");
                });

            modelBuilder.Entity("HuoltoWebApp.Models.Säiliö", b =>
                {
                    b.Navigation("Autos");

                    b.Navigation("SäiliöHuollots");

                    b.Navigation("SäiliöHuoltopyyntös");

                    b.Navigation("SäiliöInfo");

                    b.Navigation("SäiliöMuistutus");
                });
#pragma warning restore 612, 618
        }
    }
}
