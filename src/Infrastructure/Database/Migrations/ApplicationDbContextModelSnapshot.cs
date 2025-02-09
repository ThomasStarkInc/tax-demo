﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("Domain.Municipalities.Municipality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Municipalities");
                });

            modelBuilder.Entity("Domain.TaxSchedules.TaxSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDateUtc")
                        .HasColumnType("date");

                    b.Property<int>("Frequency")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("MunicipalityId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDateUtc")
                        .HasColumnType("date");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.ToTable("TaxSchedules");
                });

            modelBuilder.Entity("Domain.TaxSchedules.TaxSchedule", b =>
                {
                    b.HasOne("Domain.Municipalities.Municipality", "Municipality")
                        .WithMany("TaxSchedules")
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipality");
                });

            modelBuilder.Entity("Domain.Municipalities.Municipality", b =>
                {
                    b.Navigation("TaxSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
