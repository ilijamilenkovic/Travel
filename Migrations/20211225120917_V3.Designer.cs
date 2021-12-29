﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Travel.Migrations
{
    [DbContext(typeof(TravelContext))]
    [Migration("20211225120917_V3")]
    partial class V3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DrzavaPasos", b =>
                {
                    b.Property<int>("PodrzaneDrzaveID")
                        .HasColumnType("int");

                    b.Property<int>("PodrzaniPasosiID")
                        .HasColumnType("int");

                    b.HasKey("PodrzaneDrzaveID", "PodrzaniPasosiID");

                    b.HasIndex("PodrzaniPasosiID");

                    b.ToTable("DrzavaPasos");
                });

            modelBuilder.Entity("DrzavaTest", b =>
                {
                    b.Property<int>("PodrzaneDrzaveID")
                        .HasColumnType("int");

                    b.Property<int>("PodrzaniTestoviID")
                        .HasColumnType("int");

                    b.HasKey("PodrzaneDrzaveID", "PodrzaniTestoviID");

                    b.HasIndex("PodrzaniTestoviID");

                    b.ToTable("DrzavaTest");
                });

            modelBuilder.Entity("DrzavaVakcina", b =>
                {
                    b.Property<int>("PodrzaneDrzaveID")
                        .HasColumnType("int");

                    b.Property<int>("PodrzaneVakcineID")
                        .HasColumnType("int");

                    b.HasKey("PodrzaneDrzaveID", "PodrzaneVakcineID");

                    b.HasIndex("PodrzaneVakcineID");

                    b.ToTable("DrzavaVakcina");
                });

            modelBuilder.Entity("Models.Drzava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("Models.Pasos", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Drzavljanstvo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Pasos");
                });

            modelBuilder.Entity("Models.Test", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Tip")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Models.Vakcina", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Doza")
                        .HasColumnType("int");

                    b.Property<string>("Proizvodjac")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Vakcina");
                });

            modelBuilder.Entity("DrzavaPasos", b =>
                {
                    b.HasOne("Models.Drzava", null)
                        .WithMany()
                        .HasForeignKey("PodrzaneDrzaveID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Pasos", null)
                        .WithMany()
                        .HasForeignKey("PodrzaniPasosiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrzavaTest", b =>
                {
                    b.HasOne("Models.Drzava", null)
                        .WithMany()
                        .HasForeignKey("PodrzaneDrzaveID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Test", null)
                        .WithMany()
                        .HasForeignKey("PodrzaniTestoviID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrzavaVakcina", b =>
                {
                    b.HasOne("Models.Drzava", null)
                        .WithMany()
                        .HasForeignKey("PodrzaneDrzaveID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Vakcina", null)
                        .WithMany()
                        .HasForeignKey("PodrzaneVakcineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
