﻿// <auto-generated />
using System;
using DataCoreModule.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataCoreModule.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241027140058_EntityCountryRemoved")]
    partial class EntityCountryRemoved
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataCoreModule.Core.Models.Models.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsMonitored")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("DataCoreModule.Core.Models.Models.Entities.EnvironmentalState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AirQualityMark")
                        .HasColumnType("float");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("EnvironmentalStates");
                });

            modelBuilder.Entity("DataCoreModule.Core.Models.Models.Entities.EnvironmentalState", b =>
                {
                    b.HasOne("DataCoreModule.Core.Models.Models.Entities.City", "City")
                        .WithMany("EnvironmentalEnvironmentalStates")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("DataCoreModule.Core.Models.Models.Entities.City", b =>
                {
                    b.Navigation("EnvironmentalEnvironmentalStates");
                });
#pragma warning restore 612, 618
        }
    }
}
