using hNext.DbAccessMSSQLCore;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hNext.MSSQLCoreRepository.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void TestPut()
        {
            //Arrange
            var patients = new List<Patient>();
            //var manager = new Mock<IStateManager>();
            //var model = new Mock<Microsoft.EntityFrameworkCore.Metadata.Internal.Model>();
            //var entry = new Mock<EntityEntry<Patient>>(new InternalShadowEntityEntry(
            //    manager.Object, new EntityType("Patient", model.Object, ConfigurationSource.Convention)));
            //entry.SetupSet(e => e.State = It.IsAny<EntityState>()).Verifiable();
            var dbSet = patients.AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            var patient = new Patient();
            context.Setup(c => c.Set<Patient>()).Returns(dbSet.Object);
            //context.Setup(c => c.Entry(It.IsAny<Patient>())).Returns(entry.Object);
            Repository<Patient> repository = new Repository<Patient>(context.Object);

            //Act
            var result = repository.Put(patient).Result;

            //Assert
            Assert.AreEqual(patient, result);
            dbSet.Verify(d => d.Update(patient), Times.Once);
            context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public void TestDelete()
        {
            //Arrange
            var patients = new List<Patient>();
            int patientId = 3;
            var patient = new Patient { Id = patientId };
            var dbSet = patients.AsQueryable().BuildMockDbSet();
            dbSet.Setup(d => d.FindAsync(It.IsAny<int>())).ReturnsAsync(patient);
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.Set<Patient>()).Returns(dbSet.Object);
            Repository<Patient> repository = new Repository<Patient>(context.Object);

            //Act
            var result = repository.Delete(3).Result;

            //Assert
            Assert.AreEqual(patientId, result.Id);
            dbSet.Verify(d => d.Remove(patient), Times.Once);
            context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
