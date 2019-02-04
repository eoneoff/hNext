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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet <District> Districts { get; set; }
        public DbSet <City> Cities { get; set; }
        public DbSet<CityType> CityTypes { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<StreetType> GetStreetTypes { get; set; }
    }
}
