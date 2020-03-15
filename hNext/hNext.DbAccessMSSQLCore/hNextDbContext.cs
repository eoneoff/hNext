using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace hNext.DbAccessMSSQLCore
{
    public partial class hNextDbContext : DbContext
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
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<PersonEmails> PersonEmails { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<GuardianWard> GuardianWards { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<HospitalType> HospitalTypes { get; set; }
        public virtual DbSet<HospitalPhone> HospitalPhones { get; set; }
        public virtual DbSet<HospitalEmail> HospitalEmails { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<HospitalLicense> HospitalLicenses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentSpecialty> DepartmentSpecialties { get; set; }
        public virtual DbSet<DepartmentPhone> DepartmentPhones { get; set; }
        public virtual DbSet<DepartmentEmail> DepartmentEmails { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<DoctorPosition> DoctorPositions { get; set; }
        public virtual DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public virtual DbSet<Diploma> Diplomas { get; set; }
        public virtual DbSet<DocumentRegistry> DocumentRegistries {get;set;}
        public virtual DbSet<CaseHistory> CaseHistories { get; set; }
        public virtual DbSet<CaseHistoryAdmission> CaseHistoryAdmissions { get; set; }
        public virtual DbSet<ICD> ICD { get; set; }
        public virtual DbSet<Diagnosys> Diagnoses { get; set; }
        public virtual DbSet<PatientDiagnosys> PatientDiagnoses { get; set; }
        public virtual DbSet<CaseHistoryDiagnosys> CaseHistoryDiagnoses { get; set; }
        public virtual DbSet<RecordFieldTemplate> RecordFieldTemplates { get; set; }
        public virtual DbSet<RecordTemplate> RecordTemplates { get; set; }
        public virtual DbSet<RecordField> RecordFields { get; set; }
        public virtual  DbSet<RecordFieldTemplateOption> RecordFieldTemplateOptions { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<RecordDiagnosys> RecordDiagnoses { get; set; }
        public virtual DbSet<CaseHistoryRecord> CaseHistoryRecords { get; set; }
        public virtual DbSet<CaseHistoryConsultation> CaseHistoryConsultations { get; set; }

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

            modelBuilder.Entity<GuardianWard>().HasKey(gw => new {gw.WardId, gw.GuardianId });
            modelBuilder.Entity<GuardianWard>().HasOne(gw => gw.Guardian).WithMany(p => p.Wards)
                .HasForeignKey(gw => gw.GuardianId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<GuardianWard>().HasOne(gw => gw.Ward).WithMany(p => p.Guardians)
                .HasForeignKey(gw => gw.WardId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hospital>().HasIndex(h => h.Name);
            modelBuilder.Entity<HospitalPhone>().HasKey(hp => new { hp.HospitalId, hp.PhoneId });
            modelBuilder.Entity<HospitalEmail>().HasKey(he => new { he.HospitalId, he.EmailId });

            modelBuilder.Entity<Department>().HasIndex(d => d.Name);
            modelBuilder.Entity<Department>().HasIndex(d => d.HospitalId);
            modelBuilder.Entity<DepartmentSpecialty>().HasKey(ds => new { ds.DeparmentId, ds.SpecialtyId });
            modelBuilder.Entity<DepartmentPhone>().HasKey(dp => new { dp.DepartmentId, dp.PhoneId });
            modelBuilder.Entity<DepartmentEmail>().HasKey(de => new { de.DepartmentId, de.EmailId});

            modelBuilder.Entity<DoctorPosition>().HasIndex(dp => dp.DoctorId);
            modelBuilder.Entity<DoctorPosition>().HasIndex(dp => dp.HospitalId);
            modelBuilder.Entity<DoctorPosition>().HasIndex(dp => dp.DepartmentId);
            modelBuilder.Entity<DoctorPosition>().HasOne(dp => dp.Doctor).WithMany(d => d.DoctorPositions)
                .HasForeignKey(dp => dp.DoctorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DoctorPosition>().HasOne(dp => dp.Hospital).WithMany(h => h.DoctorPositions)
                .HasForeignKey(dp => dp.HospitalId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DoctorPosition>().HasOne(dp => dp.Department).WithMany(d => d.DoctorPositions)
                .HasForeignKey(dp => dp.DepartmentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CaseHistory>().HasIndex(h => h.PatientId);
            modelBuilder.Entity<CaseHistory>().HasIndex(h => h.HospitalId);
            modelBuilder.Entity<CaseHistory>().HasOne(h => h.Patient).WithMany(p => p.CaseHistories)
                .HasForeignKey(h => h.PatientId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CaseHistory>().HasOne(h => h.Hospital).WithMany(h => h.CaseHistories)
                .HasForeignKey(h => h.HospitalId);
            modelBuilder.Entity<CaseHistory>().HasOne(h => h.ReferredBy).WithMany(h => h.Referred)
                .HasForeignKey(h => h.ReferredById);

            modelBuilder.Entity<CaseHistoryAdmission>().HasIndex(a => a.CaseHistoryId);
            modelBuilder.Entity<ICD>().HasIndex(i => i.Letter);
            modelBuilder.Entity<Diagnosys>().HasIndex(d => d.ICDId);
            modelBuilder.Entity<PatientDiagnosys>().HasKey(pd => new { pd.PatientId, pd.DiagnosysId });
            modelBuilder.Entity<PatientDiagnosys>().Property(pd => pd.Active).HasDefaultValue(true);
            modelBuilder.Entity<CaseHistoryDiagnosys>().HasKey(hd => new { hd.CaseHistoryId, hd.DiagnosysId });
            modelBuilder.Entity<CaseHistoryDiagnosys>().Property(hd => hd.Active).HasDefaultValue(true);

            modelBuilder.Entity<RecordFieldTemplate>().HasIndex(t => t.RecordTemplateId);
            modelBuilder.Entity<RecordFieldTemplate>().Property(t => t.NewLine).HasDefaultValue(false);
            modelBuilder.Entity<RecordFieldTemplateOption>().HasIndex(o => o.RecordFieldTemplateId);
            modelBuilder.Entity<RecordTemplate>().HasIndex(t => t.HospitalId);
            modelBuilder.Entity<RecordTemplate>().HasIndex(t => t.DepartmentId);
            modelBuilder.Entity<RecordTemplate>().HasIndex(t => t.SpecialtyId);
            modelBuilder.Entity<RecordTemplate>().HasIndex(t => t.DoctorId);
            modelBuilder.Entity<RecordDiagnosys>().HasKey(rd => new { rd.RecordId, rd.DiagnosysId });
            modelBuilder.Entity<RecordField>().HasOne(f => f.Record).WithMany(r => r.RecordFields)
                .HasForeignKey(f => f.RecordId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RecordField>().HasIndex(f => f.RecordId);
            modelBuilder.Entity<Record>().HasIndex(r => r.PatientId);

            modelBuilder.Entity<CaseHistoryRecord>().Property(r => r.CaseHistoryId).HasColumnName("CaseHistoryId");
            modelBuilder.Entity<CaseHistoryConsultation>().Property(r => r.CaseHistoryId).HasColumnName("CaseHistoryId");
            modelBuilder.Entity<CaseHistoryRecord>().HasOne(r => r.CaseHistory).WithMany(h => h.Records)
                .HasForeignKey(r => r.CaseHistoryId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CaseHistoryConsultation>().HasOne(r => r.CaseHistory).WithMany(h => h.Consultations)
                .HasForeignKey(r => r.CaseHistoryId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CaseHistoryRecord>().HasIndex(r => r.CaseHistoryId);
            modelBuilder.Entity<CaseHistoryConsultation>().HasIndex(r => r.CaseHistoryId);
        }
    }
}
