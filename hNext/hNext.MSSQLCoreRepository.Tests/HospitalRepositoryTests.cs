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
    public class HospitalRepositoryTests
    {
        Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void ExistsChecksCorrectly()
        {
            //Arrange
            string name = "test name";
            var hospitals = new List<Hospital>
            {
                new Hospital{Name = name},
                new Hospital{Name = $"aaa{name}"},
                new Hospital{Name = $"{name}bbb"}
            };
            var dbSet = hospitals.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<Hospital>()).Returns(dbSet.Object);
            IHospitalRepository repository = new HospitalRepository(context.Object);

            //Act
            var result = repository.Exists(name).Result;

            //Assert
            Assert.IsTrue(result);
        }
    }
}
