using hNext.DataService.Controllers;
using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.DataService.Tests
{
    [TestClass]
    public class PatientsControllerTests
    {
        private Mock<IPatientsRepository> repository = new Mock<IPatientsRepository>();
        private Mock<IRepository<PatientDiagnosys>> diagnosysRepository = new Mock<IRepository<PatientDiagnosys>>();
        private PatientsController controller;

        public PatientsControllerTests()
        {
            controller = new PatientsController(repository.Object, diagnosysRepository.Object);
        }

        [TestMethod]
        public void GetReturnsListOfPatients()
        {
            //Arrange
            repository.Setup(m => m.Get()).Returns(Task.FromResult(new List<Patient>() as IEnumerable<Patient>));

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Patient>));
        }

        [TestMethod]
        public void GetIdReturnsPatient()
        {
            //Arrange
            repository.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Patient { Id = (int)id[0] }));
            int patientId = 3;

            //Act
            var result = controller.Get(patientId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Patient));
            Assert.AreEqual(patientId, result.Id);
        }

        [TestMethod]
        public void PatientSearchTest()
        {
            //Arrange
            repository.Setup(m => m.SearchPatients(It.IsAny<PatientSearchModel>())).Returns(Task.FromResult(new List<Patient>() as IEnumerable<Patient>));

            //Act
            var result = controller.Search(new PatientSearchModel()).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Patient>));
        }

        [TestMethod]
        public void GetDiagnosesReturnsListOfPatientDiagnoses()
        {
            //Arrange
            repository.Setup(r => r.GetDiagnoses(It.IsAny<long>())).ReturnsAsync(new List<PatientDiagnosys>() as IEnumerable<PatientDiagnosys>);

            //Act
            var result = controller.GetDiagnoses(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<PatientDiagnosys>));
        }

        [TestMethod]
        public void AddDiagnosysReturnsPatientDiagnosys()
        {
            //Arrange
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            diagnosysRepository.Setup(dr => dr.Exists(It.IsAny<object[]>())).ReturnsAsync(false);
            diagnosysRepository.Setup(dr => dr.Post(It.IsAny<PatientDiagnosys>())).ReturnsAsync((PatientDiagnosys d) =>
            { return d; });
            long patientId = 1;

            //Act
            var result = (controller.AddDiagnosys(patientId, new PatientDiagnosys()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(PatientDiagnosys));
            Assert.AreEqual(patientId, (result as PatientDiagnosys).PatientId);
        }

        [TestMethod]
        public void RemoveDiagnosysReturnsPatientDiagnosys()
        {
            //Arrange
            diagnosysRepository.Setup(dr => dr.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            diagnosysRepository.Setup(dr => dr.Delete(It.IsAny<object[]>())).ReturnsAsync(new PatientDiagnosys());

            //Act
            var result = (controller.RemoveDiagnosys(1, 1).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(PatientDiagnosys));
        }
    }
}
