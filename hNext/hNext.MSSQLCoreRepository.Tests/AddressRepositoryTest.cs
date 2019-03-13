using hNext.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MockQueryable.Moq;
using hNext.DbAccessMSSQLCore;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace hNext.MSSQLCoreRepository.Tests
{
    [TestClass]
    public class AddressRepositoryTest
    {
        [TestMethod]
        public void ExistsChecksCorrectly()
        {
            //Arrange
            long addressId = 3;
            var address = new Address
            {
                Id = addressId,
                CountryId = 1,
                CityId = 1,
                StreetId = 1,
                Building = "test",
                Apartment = "test"
            };
            var addresses = new List<Address>
            {
                address
            };
            var dbSet = addresses.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Set<Address>()).Returns(dbSet.Object);
            AddressRepository repository = new AddressRepository(context.Object);

            //Act
            var found = repository.Exists(address).Result;
            var notFound = repository.Exists(new Address()).Result;

            //Assert
            Assert.AreEqual(found, addressId);
            Assert.IsNull(notFound);
        }
    }
}
