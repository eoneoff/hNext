using hNext.DbAccessMSSQLCore;
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
    public class RegionsRepositoryTests
    {
        [TestMethod]
        public void TestDistrictsGetter()
        {
            //Arrange
            var districts = new List<District>
            {
                new District { RegionId = 1 },
                new District { RegionId = 2 },
                new District { RegionId = 1 },
                new District { RegionId = 3 }
            };
            var dbSet = districts.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Districts).Returns(dbSet.Object);
            RegionsRepository repository = new RegionsRepository(context.Object);

            //Act
            var result = repository.GetDistricts(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<District>));
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.All(c => c.RegionId == 1));
        }

        [TestMethod]
        public void TestCityGetter()
        {
            // Arrange
            var cities = new List<City>
            {
                new City { RegionId = 1 },
                new City { RegionId = 2 },
                new City { RegionId = 1 },
                new City { RegionId = 3 }
            };
            var dbSet = cities.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Cities).Returns(dbSet.Object);
            RegionsRepository repository = new RegionsRepository(context.Object);

            //Act
            var result = repository.GetCities(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<City>));
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.All(c => c.RegionId == 1));
        }
    }
}
