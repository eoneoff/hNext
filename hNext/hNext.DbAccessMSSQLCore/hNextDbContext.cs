using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace hNext.DbAccessMSSQLCore
{
    public class hNextDbContext:DbContext
    {
        public hNextDbContext(DbContextOptions<hNextDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityType> CityTypes { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<StreetType> StreetTypes { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<PersonPhone> PersonPhones { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasIndex(p => p.FamilyName);
            modelBuilder.Entity<Person>().HasIndex(p => p.DateOfBirth);

            modelBuilder.Entity<Address>().HasOne(a => a.Country).WithMany(c => c.Addresses).HasForeignKey(a => a.CountryId).
                OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Person>().HasOne(p => p.CountryOfBirth).WithMany(c => c.PeopleBorn).
                HasForeignKey(p => p.CountryOfBirthId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Address>().HasIndex(a => a.CityId);

            modelBuilder.Entity<Address>().HasOne(a => a.Region).WithMany(r => r.Addresses).
                HasForeignKey(a => a.RegionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Region>().HasOne(r => r.Country).WithMany(c => c.Regions).
                HasForeignKey(r => r.CountryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>().HasOne(a => a.District).WithMany(d => d.Addresses).
                HasForeignKey(a => a.DistrictId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<District>().HasOne(d => d.Region).WithMany(r => r.Districts).
                HasForeignKey(d => d.RegionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<District>().HasIndex(d => d.Name);

            modelBuilder.Entity<Person>().HasOne(p => p.PlaceOfBirth).WithMany(c => c.PeopleBorn).
                HasForeignKey(p => p.PlaceOfBirthId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Address>().HasOne(a => a.City).WithMany(c => c.Addresses).
                HasForeignKey(a => a.CityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<City>().HasOne(c => c.Country).WithMany(c => c.Cities).
                HasForeignKey(c => c.CountryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<City>().HasOne(c => c.Region).WithMany(r => r.Cities).
                HasForeignKey(c => c.RegionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<City>().HasOne(c => c.District).WithMany(d => d.Cities).
                HasForeignKey(c => c.DistrictId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<City>().HasIndex(c => c.Name);
            modelBuilder.Entity<City>().HasIndex(c => c.RegionId);

            modelBuilder.Entity<Address>().HasOne(a => a.Street).WithMany(s => s.Addresses).
                HasForeignKey(a => a.StreetId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Street>().HasOne(s => s.City).WithMany(c => c.Streets).
                HasForeignKey(s => s.CityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Street>().HasIndex(s => s.Name);

            modelBuilder.Entity<Phone>().HasIndex(p => p.Number);

            modelBuilder.Entity<PersonPhone>().HasKey(pp => new { pp.PersonId, pp.PhoneId });

            modelBuilder.Entity<PersonEmails>().HasKey(pe => new { pe.PersonId, pe.EmailId });

            modelBuilder.Entity<GuardianWard>().HasKey(gw => new {gw.GuardianId, gw.WardId });
            modelBuilder.Entity<GuardianWard>().HasOne(gw => gw.Guardian).WithMany(p => p.Wards)
                .HasForeignKey(gw => gw.GuardianId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<GuardianWard>().HasOne(gw => gw.Ward).WithMany(p => p.Guardians)
                .HasForeignKey(gw => gw.WardId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
