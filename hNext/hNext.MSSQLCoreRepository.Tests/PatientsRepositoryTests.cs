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
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void PatientSearchTest()
        {
            //Arrange
            var patients = new List<Patient>().AsQueryable().BuildMockDbSet();
            context.Setup(c => c.SearchPatients(It.IsAny<PatientSearchModel>())).Returns(patients.Object);
            PatientsRepository repository = new PatientsRepository(context.Object);

            //Act
            var result = repository.SearchPatients(new PatientSearchModel()).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Patient>));
        }

        [TestMethod]
        public void GetDiangosesReturnsListOfPatientDiagnoses()
        {
            //Arrange
            long patientId = 1;
            var diagnoses = new List<PatientDiagnosys>
            {
                new PatientDiagnosys{PatientId = patientId},
                new PatientDiagnosys{PatientId = patientId +1},
                new PatientDiagnosys{PatientId = patientId},
                new PatientDiagnosys{PatientId = patientId + 2}
            };
            var dbSet = diagnoses.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.PatientDiagnoses).Returns(dbSet.Object);
            PatientsRepository repository = new PatientsRepository(context.Object);

            //Act
            var result = repository.GetDiagnoses(patientId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<PatientDiagnosys>));
            Assert.AreEqual(2, result.Count());
        }
    }
}
