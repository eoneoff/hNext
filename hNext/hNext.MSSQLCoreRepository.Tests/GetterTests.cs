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
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository.Tests
{
    [TestClass]
    public class GetterTests
    {
        [TestMethod]
        public void GetReturnsIEnumerable()
        {
            //Arrange
            var patients = new List<Patient>
            {
                new Patient(),
                new Patient(),
                new Patient()
            };

            var dbSet = patients.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Set<Patient>()).Returns(dbSet.Object);
            Getter<Patient> getter = new Getter<Patient>(context.Object);

            //Act
            var result = getter.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Patient>));
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        public void GetIdReturnsCorrect()
        {
            //Arrange
            var patients = new List<Patient>();

            var dbSet = patients.AsQueryable().BuildMockDbSet();
            int patientId = 2;
            dbSet.Setup(d => d.FindAsync(It.IsAny<int>()))
                .Returns<object>(id => Task.FromResult(new Patient { Id = patientId}));
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Set<Patient>()).Returns(dbSet.Object);
            Getter<Patient> getter = new Getter<Patient>(context.Object);

            //Act
            var result = getter.Get(patientId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Patient));
            Assert.AreEqual(result.Id, patientId);
        }
    }
}
