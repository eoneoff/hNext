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
using System.Threading;

namespace hNext.MSSQLCoreRepository.Tests
{
    [TestClass]
    public class PosterTest
    {
        [TestMethod]
        public void TestPost()
        {
            //Arrange
            var patients = new List<Patient>();
            var dbSet = patients.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Set<Patient>()).Returns(dbSet.Object);
            Poster<Patient> poster = new Poster<Patient>(context.Object);
            var patient = new Patient();

            //Act
            var result = poster.Post(patient).Result;

            //Assert
            dbSet.Verify(d => d.Update(patient), Times.Once);
            context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.AreEqual(patient, result);
        }
    }
}
