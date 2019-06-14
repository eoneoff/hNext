using hNext.DbAccessMSSQLCore;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
    public class CityRepositoryTest
    {
        [TestMethod]
        public void TestStreetsGetter()
        {
            //Arrange
            var streets = new List<Street>
            {
                new Street { CityId = 1 },
                new Street { CityId = 2 },
                new Street { CityId = 1 },
                new Street { CityId = 3 }
            };
            var dbSet = streets.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Streets).Returns(dbSet.Object);
            CityRepository repository = new CityRepository(context.Object);

            //Act
            var result = repository.GetStreets(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Street>));
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result.All(c => c.CityId == 1));
        }
    }
}
