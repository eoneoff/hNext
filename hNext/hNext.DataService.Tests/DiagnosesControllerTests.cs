using hNext.DataService.Controllers;
using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.DataService.Tests
{
    [TestClass]
    public class DiagnosesControllerTests
    {
        private Mock<IPoster<Diagnosys>> repository = new Mock<IPoster<Diagnosys>>();
        private DiagnosesController controller;

        public DiagnosesControllerTests()
        {
            controller = new DiagnosesController(repository.Object);
        }

        [TestMethod]
        public void GetReturnsListOfDiagnoses()
        {
            //Arrange
            repository.Setup(r => r.Get()).ReturnsAsync(new List<Diagnosys>() as IEnumerable<Diagnosys>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Diagnosys>));
        }

        [TestMethod]
        public void GetIdRetursDiagnosys()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Diagnosys
                {
                    Id = (long)id[0]
                };
            });
            long diagnosysId = 1;

            //Act
            var result = controller.Get(diagnosysId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Diagnosys));
            Assert.AreEqual(diagnosysId, result.Id);
        }

        [TestMethod]
        public void PostReturnsDiagnosys()
        {
            //Arrange
            repository.Setup(r => r.Post(It.IsAny<Diagnosys>())).ReturnsAsync((Diagnosys d) => { return d; });

            //Act
            var result = (controller.Post(new Diagnosys()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Diagnosys));
        }
    }
}
