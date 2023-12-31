﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace UrlShort.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("UrlShort.Models.InfoCounter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Laptop")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Other")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShortUrlId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Smartphone")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tablet")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ShortUrlId");

                    b.ToTable("InfoCounters");
                });

            modelBuilder.Entity("UrlShort.Models.IpInfo", b =>
                {
                    b.Property<int>("IpInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Continent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Isp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<int>("ShortUrlId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IpInfoId");

                    b.HasIndex("ShortUrlId");

                    b.ToTable("IpInfos");
                });

            modelBuilder.Entity("UrlShort.Models.Stat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BrowserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("BrowserVersion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceFamily")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OperatingSystem")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OperatingSystemVirsion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ShortUrlId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ShortUrlId");

                    b.ToTable("Stat");
                });

            modelBuilder.Entity("UrlShort.Models.UrlManagment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomShortUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("QrCodeImage")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("ShortUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Visits")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Urls");
                });

            modelBuilder.Entity("UrlShort.Models.InfoCounter", b =>
                {
                    b.HasOne("UrlShort.Models.UrlManagment", "ShortUrl")
                        .WithMany("InfoCounter")
                        .HasForeignKey("ShortUrlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShortUrl");
                });

            modelBuilder.Entity("UrlShort.Models.IpInfo", b =>
                {
                    b.HasOne("UrlShort.Models.UrlManagment", "ShortUrl")
                        .WithMany("IpInfo")
                        .HasForeignKey("ShortUrlId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ShortUrl");
                });

            modelBuilder.Entity("UrlShort.Models.Stat", b =>
                {
                    b.HasOne("UrlShort.Models.UrlManagment", "ShortUrl")
                        .WithMany("Stat")
                        .HasForeignKey("ShortUrlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShortUrl");
                });

            modelBuilder.Entity("UrlShort.Models.UrlManagment", b =>
                {
                    b.Navigation("InfoCounter");

                    b.Navigation("IpInfo");

                    b.Navigation("Stat");
                });
#pragma warning restore 612, 618
        }
    }
}
