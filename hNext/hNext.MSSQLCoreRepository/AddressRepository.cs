using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(hNextDbContext db) : base(db) { }

        public override async Task<Address> Post(Address item)
        {
            dbSet.Update(item);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(a=> a.Country)
                .Include(a => a.Region)
                .Include(a => a.District)
                .Include(a => a.City).ThenInclude(c => c.CityType)
                .Include(a => a.Street).ThenInclude(s => s.StreetType)
                .AsNoTracking().SingleOrDefaultAsync(a => a.Id == item.Id);
        }

        public override async Task<Address> Put(Address item)
        {
            dbSet.Update(item);
            await db.SaveChangesAsync();
            return await dbSet
                .Include(a => a.Country)
                .Include(a => a.Region)
                .Include(a => a.District)
                .Include(a => a.City).ThenInclude(c => c.CityType)
                .Include(a => a.Street).ThenInclude(s => s.StreetType)
                .AsNoTracking().SingleOrDefaultAsync(a => a.Id == item.Id);
        }

        public async Task<Address> Exists(Address address)
        {
            if(string.IsNullOrWhiteSpace(address.Building))
            {
                address.Building = null;
            }
            if(string.IsNullOrWhiteSpace(address.Apartment))
            {
                address.Apartment = null;
            }
            return await dbSet.AsNoTracking()
                .Include(a => a.Country)
                .Include(a => a.Region)
                .Include(a => a.District)
                .Include(a => a.City).ThenInclude(c => c.CityType)
                .Include(a => a.Street).ThenInclude(s => s.StreetType)
                .SingleOrDefaultAsync(a => a.Id != address.Id
                                                    && a.CountryId == address.CountryId
                                                    && a.CityId == address.CityId
                                                    && a.StreetId == address.StreetId
                                                    && a.Building == address.Building
                                                    && a.Apartment == address.Apartment);
        }
    }
}
