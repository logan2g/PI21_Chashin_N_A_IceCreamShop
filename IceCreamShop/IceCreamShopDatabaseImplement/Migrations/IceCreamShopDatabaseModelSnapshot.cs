﻿// <auto-generated />
using System;
using IceCreamShopDatabaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IceCreamShopDatabaseImplement.Migrations
{
    [DbContext(typeof(IceCreamShopDatabase))]
    partial class IceCreamShopDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientFIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.IceCream", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IceCreamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("IceCreams");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.IceCreamComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComponentId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("IceCreamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("IceCreamId");

                    b.ToTable("IceCreamComponents");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.Implementer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImplementerFIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PauseTime")
                        .HasColumnType("int");

                    b.Property<int>("WorkingTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Implementers");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateImplement")
                        .HasColumnType("datetime2");

                    b.Property<int>("IceCreamId")
                        .HasColumnType("int");

                    b.Property<string>("IceCreamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImplementerId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("IceCreamId");

                    b.HasIndex("ImplementerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResponsiblePerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.WarehouseComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComponentId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseComponents");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.IceCreamComponent", b =>
                {
                    b.HasOne("IceCreamShopDatabaseImplement.Models.Component", "Component")
                        .WithMany("IceCreamComponents")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IceCreamShopDatabaseImplement.Models.IceCream", "IceCream")
                        .WithMany("IceCreamComponents")
                        .HasForeignKey("IceCreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.Order", b =>
                {
                    b.HasOne("IceCreamShopDatabaseImplement.Models.Client", "Client")
                        .WithMany("Order")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IceCreamShopDatabaseImplement.Models.IceCream", "IceCream")
                        .WithMany("Orders")
                        .HasForeignKey("IceCreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IceCreamShopDatabaseImplement.Models.Implementer", "Implementer")
                        .WithMany("Order")
                        .HasForeignKey("ImplementerId");
                });

            modelBuilder.Entity("IceCreamShopDatabaseImplement.Models.WarehouseComponent", b =>
                {
                    b.HasOne("IceCreamShopDatabaseImplement.Models.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IceCreamShopDatabaseImplement.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseComponents")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
