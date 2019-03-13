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
            return await AddressChange(item, (address) => dbSet.Add(address));
        }

        public override async Task<Address> Put(Address item)
        {
            return await AddressChange(item, (address) => db.Entry(address).State = EntityState.Modified);
        }

        public async Task<long?> Exists(Address address)
        {
            return (await dbSet.SingleOrDefaultAsync(a => a.CountryId == address.CountryId
                                                    && a.CityId == address.CityId
                                                    && a.StreetId == address.StreetId
                                                    && a.Building == address.Building
                                                    && a.Apartment == address.Apartment))?.Id;
        }

        private async Task<Address> AddressChange(Address address, Action<Address> action)
        {
            Address properyStore = new Address();
            StoreDependencyProperties(address, properyStore);

            action(address);
            await db.SaveChangesAsync();

            return await dbSet
            .Include(a => a.Region)
            .Include(a => a.District)
            .Include(a => a.City).ThenInclude(c => c.CityType)
            .Include(a => a.Street).ThenInclude(s => s.StreetType)
            .AsNoTracking().SingleOrDefaultAsync(a => a.Id == address.Id);
        }

        private void StoreDependencyProperties(Address source, Address destination)
        {
            destination.Country = source.Country;
            source.Country = null;
            destination.Region = source.Region;
            source.Region = null;
            destination.District = source.District;
            source.District = null;
            destination.City = source.City;
            source.City = null;
            destination.Street = source.Street;
            source.Street = null;
            destination.AddressType = source.AddressType;
            source.AddressType = null;
            destination.People = source.People;
            source.People = null;
        }
    }
}
