﻿// <auto-generated />
using System;
using GuarderiaApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GuarderiaApp.Migrations
{
    [DbContext(typeof(GuarderiaDbContext))]
    partial class GuarderiaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GuarderiaApp.Models.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlatoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlatoId");

                    b.ToTable("Ingredientes");
                });

            modelBuilder.Entity("GuarderiaApp.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("GuarderiaApp.Models.MenuConsumido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("NiñoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("NiñoId");

                    b.ToTable("MenusConsumidos");
                });

            modelBuilder.Entity("GuarderiaApp.Models.Niño", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alergias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaBaja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroMatricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Niños");
                });

            modelBuilder.Entity("GuarderiaApp.Models.PersonaAutorizada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PersonasAutorizadas");
                });

            modelBuilder.Entity("GuarderiaApp.Models.Plato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Platos");
                });

            modelBuilder.Entity("NiñoPersonaAutorizada", b =>
                {
                    b.Property<int>("NiñosId")
                        .HasColumnType("int");

                    b.Property<int>("PersonasAutorizadasId")
                        .HasColumnType("int");

                    b.HasKey("NiñosId", "PersonasAutorizadasId");

                    b.HasIndex("PersonasAutorizadasId");

                    b.ToTable("NiñoPersonaAutorizada");
                });

            modelBuilder.Entity("GuarderiaApp.Models.Ingrediente", b =>
                {
                    b.HasOne("GuarderiaApp.Models.Plato", null)
                        .WithMany("Ingredientes")
                        .HasForeignKey("PlatoId");
                });

            modelBuilder.Entity("GuarderiaApp.Models.MenuConsumido", b =>
                {
                    b.HasOne("GuarderiaApp.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuarderiaApp.Models.Niño", "Niño")
                        .WithMany("MenusConsumidos")
                        .HasForeignKey("NiñoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Niño");
                });

            modelBuilder.Entity("GuarderiaApp.Models.Plato", b =>
                {
                    b.HasOne("GuarderiaApp.Models.Menu", null)
                        .WithMany("Platos")
                        .HasForeignKey("MenuId");
                });

            modelBuilder.Entity("NiñoPersonaAutorizada", b =>
                {
                    b.HasOne("GuarderiaApp.Models.Niño", null)
                        .WithMany()
                        .HasForeignKey("NiñosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuarderiaApp.Models.PersonaAutorizada", null)
                        .WithMany()
                        .HasForeignKey("PersonasAutorizadasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GuarderiaApp.Models.Menu", b =>
                {
                    b.Navigation("Platos");
                });

            modelBuilder.Entity("GuarderiaApp.Models.Niño", b =>
                {
                    b.Navigation("MenusConsumidos");
                });

            modelBuilder.Entity("GuarderiaApp.Models.Plato", b =>
                {
                    b.Navigation("Ingredientes");
                });
#pragma warning restore 612, 618
        }
    }
}
