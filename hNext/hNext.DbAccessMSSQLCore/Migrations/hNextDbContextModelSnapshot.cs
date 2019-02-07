﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hNext.DbAccessMSSQLCore;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    [DbContext(typeof(hNextDbContext))]
    partial class hNextDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("hNext.Model.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressTypeId");

                    b.Property<string>("Apartment")
                        .HasMaxLength(50);

                    b.Property<string>("Building")
                        .HasMaxLength(50);

                    b.Property<int>("CityId");

                    b.Property<int>("CountryId");

                    b.Property<int?>("DistrictId");

                    b.Property<int?>("RegionId");

                    b.Property<int?>("StreetId");

                    b.Property<string>("Zip")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("AddressTypeId");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("RegionId");

                    b.HasIndex("StreetId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("hNext.Model.AddressType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("AddressTypes");
                });

            modelBuilder.Entity("hNext.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityTypeId");

                    b.Property<int>("CountryId");

                    b.Property<int?>("DistrictId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("RegionId");

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CityTypeId");

                    b.HasIndex("CountryId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("Name");

                    b.HasIndex("RegionId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("hNext.Model.CityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("eHealthId")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("CityTypes");
                });

            modelBuilder.Entity("hNext.Model.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("hNext.Model.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("RegionId");

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("RegionId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("hNext.Model.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("hNext.Model.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login");

                    b.Property<long>("PersonId");

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("hNext.Model.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AddressId");

                    b.Property<int?>("CountryOfBirthId");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("FamilyName")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("GenderId");

                    b.Property<string>("Patronimic");

                    b.Property<int?>("PlaceOfBirthId");

                    b.Property<string>("TaxId")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CountryOfBirthId");

                    b.HasIndex("DateOfBirth");

                    b.HasIndex("FamilyName");

                    b.HasIndex("GenderId");

                    b.HasIndex("PlaceOfBirthId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("hNext.Model.PersonPhone", b =>
                {
                    b.Property<long>("PersonId");

                    b.Property<long>("PhoneId");

                    b.HasKey("PersonId", "PhoneId");

                    b.HasIndex("PhoneId");

                    b.ToTable("PersonPhones");
                });

            modelBuilder.Entity("hNext.Model.Phone", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<int>("PhoneTypeId");

                    b.HasKey("Id");

                    b.HasIndex("Number");

                    b.HasIndex("PhoneTypeId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("hNext.Model.PhoneType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("eHealthName")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PhoneTypes");
                });

            modelBuilder.Entity("hNext.Model.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("hNext.Model.Street", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("StreetTypeId");

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("Name");

                    b.HasIndex("StreetTypeId");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("hNext.Model.StreetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("eHealthId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("StreetTypes");
                });

            modelBuilder.Entity("hNext.Model.Address", b =>
                {
                    b.HasOne("hNext.Model.AddressType", "AddressType")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("hNext.Model.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("hNext.Model.Country", "Country")
                        .WithMany("Addresses")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("hNext.Model.District", "District")
                        .WithMany("Addresses")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("hNext.Model.Region", "Region")
                        .WithMany("Addresses")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("hNext.Model.Street", "Street")
                        .WithMany("Addresses")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("hNext.Model.City", b =>
                {
                    b.HasOne("hNext.Model.CityType", "CityType")
                        .WithMany("Cities")
                        .HasForeignKey("CityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("hNext.Model.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("hNext.Model.District", "District")
                        .WithMany("Cities")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("hNext.Model.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("hNext.Model.District", b =>
                {
                    b.HasOne("hNext.Model.Region", "Region")
                        .WithMany("Districts")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("hNext.Model.Patient", b =>
                {
                    b.HasOne("hNext.Model.Person", "Person")
                        .WithOne("Patient")
                        .HasForeignKey("hNext.Model.Patient", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("hNext.Model.Person", b =>
                {
                    b.HasOne("hNext.Model.Address", "Address")
                        .WithMany("People")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("hNext.Model.Country", "CountryOfBirth")
                        .WithMany("PeopleBorn")
                        .HasForeignKey("CountryOfBirthId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("hNext.Model.Gender", "Gender")
                        .WithMany("People")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("hNext.Model.City", "PlaceOfBirth")
                        .WithMany("PeopleBorn")
                        .HasForeignKey("PlaceOfBirthId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("hNext.Model.PersonPhone", b =>
                {
                    b.HasOne("hNext.Model.Person", "Person")
                        .WithMany("Phones")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("hNext.Model.Phone", "Phone")
                        .WithMany("People")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("hNext.Model.Phone", b =>
                {
                    b.HasOne("hNext.Model.PhoneType", "PhoneType")
                        .WithMany("Phones")
                        .HasForeignKey("PhoneTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("hNext.Model.Region", b =>
                {
                    b.HasOne("hNext.Model.Country", "Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("hNext.Model.Street", b =>
                {
                    b.HasOne("hNext.Model.City", "City")
                        .WithMany("Streets")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("hNext.Model.StreetType", "StreetType")
                        .WithMany("Streets")
                        .HasForeignKey("StreetTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
