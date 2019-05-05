using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hNext.MSSQLCoreRepository.Tests
{
    [TestClass]
    public class CountryRepositoryTests
    {
        [TestMethod]
        public void TestRegionsGetter()
        {
            //Arrange
            var regions = new List<Region>
            {
                new Region { CountryId = 1 },
                new Region { CountryId = 2 },
                new Region { CountryId = 1 },
                new Region { CountryId = 3 }
            };
            var dbSet = regions.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Regions).Returns(dbSet.Object);
            CountryRepository repository = new CountryRepository(context.Object);

            //Act
            var result = repository.GetRegions(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Region>));
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.All(c => c.CountryId == 1));
        }

        [TestMethod]
        public void TestCitiesGetter()
        {
            //Arrange
            var regions = new List<City>
            {
                new City { CountryId = 1 },
                new City { CountryId = 2 },
                new City { CountryId = 1 },
                new City { CountryId = 3 }
            };
            var dbSet = regions.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Cities).Returns(dbSet.Object);
            CountryRepository repository = new CountryRepository(context.Object);

            //Act
            var result = repository.GetCities(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<City>));
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.All(c => c.CountryId == 1));
        }

        [TestMethod]
        public void TestCitiesByNameGetter()
        {
            //Arrange
            int countryId = 1;
            string name = "aaa";
            var regions = new List<City>
            {
                new City { CountryId = countryId, Name = name },
                new City { CountryId = countryId + 1, Name = name },
                new City { CountryId = countryId, Name=$"ccc{name}" },
                new City { CountryId = countryId + 2, Name=$"bbb{name}" }
            };
            var dbSet = regions.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Cities).Returns(dbSet.Object);
            ICountryRepository repository = new CountryRepository(context.Object);

            //Act
            var result = repository.GetCitiesByName(countryId, name).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<City>));
            Assert.AreEqual(result.Count(), 1);
            Assert.IsTrue(result.All(c => c.CountryId == countryId));
            Assert.IsTrue(result.All(c => c.Name.ToLower().StartsWith(name.ToLower())));
        }
    }
}
