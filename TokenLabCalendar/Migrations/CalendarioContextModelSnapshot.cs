﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TokenLabCalendar.Models;

namespace TokenLabCalendar.Migrations
{
    [DbContext(typeof(CalendarioContext))]
    partial class CalendarioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TokenLabCalendar.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<string>("Location");

                    b.Property<DateTime>("StartDateTime");

                    b.Property<string>("Title");

                    b.Property<int?>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TokenLabCalendar.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("TokenLabCalendar.Models.Event", b =>
                {
                    b.HasOne("TokenLabCalendar.Models.Usuario", "Usuario")
                        .WithMany("Events")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
