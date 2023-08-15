﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace UrlShort.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20230814134835_Fourth4-miggration")]
    partial class Fourth4miggration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("UrlShort.Models.IpInfo", b =>
                {
                    b.Property<int>("IpInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ISP")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("continent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IpInfoId");

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

                    b.Property<int>("IpInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShortUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Visits")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IpInfoId");

                    b.ToTable("Urls");
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
                    b.HasOne("UrlShort.Models.IpInfo", "IpInfo")
                        .WithMany()
                        .HasForeignKey("IpInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IpInfo");
                });

            modelBuilder.Entity("UrlShort.Models.UrlManagment", b =>
                {
                    b.Navigation("Stat");
                });
#pragma warning restore 612, 618
        }
    }
}
