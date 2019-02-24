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
    public class DistrictsRepositoryTests
    {
        [TestMethod]
        public void TestCityiesGetter()
        {
            //Arrange
            var cities = new List<City>
            {
                new City { DistrictId = 1 },
                new City { DistrictId = 2 },
                new City { DistrictId = 1 },
                new City { DistrictId = 3 }
            };
            var dbSet = cities.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Cities).Returns(dbSet.Object);
            DistrictsRepository repository = new DistrictsRepository(context.Object);

            //Act
            var result = repository.GetCities(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<City>));
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.All(c => c.DistrictId == 1));
        }
    }
}
