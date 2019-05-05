using hNext.DataService.Controllers;
using hNext.IRepository;
using hNext.Model;
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
        [TestMethod]
        public void GetReturnsListOfPatients()
        {
            //Arrange
            var moq = new Mock<IPatientsRepository>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Patient>() as IEnumerable<Patient>));
            PatientsController controller = new PatientsController(moq.Object);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Patient>));
        }

        [TestMethod]
        public void GetIdReturnsPatient()
        {
            //Arrange
            var moq = new Mock<IPatientsRepository>();
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Patient { Id = (int)id[0] }));
            PatientsController controller = new PatientsController(moq.Object);
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
            var moq = new Mock<IPatientsRepository>();
            moq.Setup(m => m.SearchPatients(It.IsAny<PatientSearchModel>())).Returns(Task.FromResult(new List<Patient>() as IEnumerable<Patient>));
            PatientsController controller = new PatientsController(moq.Object);

            //Act
            var result = controller.Search(new PatientSearchModel()).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Patient>));
        }
    }
}
