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
    public class PatientsRepositoryTests
    {
        [TestMethod]
        public void PatientSearchTest()
        {
            //Arrange
            var patients = new List<Patient>().AsQueryable().BuildMockDbSet();
            var context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
            context.Setup(c => c.SearchPatients(It.IsAny<PatientSearchModel>())).Returns(patients.Object);
            PatientsRepository repository = new PatientsRepository(context.Object);

            //Act
            var result = repository.SearchPatients(new PatientSearchModel()).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Patient>));
        }
    }
}
