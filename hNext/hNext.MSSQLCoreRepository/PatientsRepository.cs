﻿using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class PatientsRepository  :Repository<Patient>, IPatientsRepository
    {
        public PatientsRepository(hNextDbContext db) : base(db) { }

        public async override Task<IEnumerable<Patient>> Get() => await db.Patients
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
            .AsNoTracking().ToListAsync();

        public async override Task<Patient> Get(int id) => await db.Patients
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Country)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Region)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.District)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.City)
            .Include(p => p.Person).ThenInclude(p => p.Address).ThenInclude(a => a.Street)
            .AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
    }
}
