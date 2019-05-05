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
    public class GuardiansControllerTests
    {
        private Mock<IGuardianRepository> moq;
        private GuardiansController controller;

        public GuardiansControllerTests()
        {
            moq = new Mock<IGuardianRepository>();
            controller = new GuardiansController(moq.Object);
        }

        [TestMethod]
        public void GetReturnsListOfGuardianWards()
        {
            //Arrange
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<GuardianWard>() as IEnumerable<GuardianWard>));

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<GuardianWard>));
        }

        [TestMethod]
        public void GetIdReturnsGuardianWard()
        {
            //Arrange
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(
                id => Task.FromResult(new GuardianWard { WardId = (long)id[0], GuardianId = (long)id[1] }));
            long wardId = 1;
            long guardianId = wardId + 1;
            var guardian = new GuardianWard
            {
                WardId = wardId,
                GuardianId = guardianId
            };

            //Act
            var result = controller.Get(wardId, guardianId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(GuardianWard));
            Assert.AreEqual(wardId, result.WardId);
            Assert.AreEqual(guardianId, result.GuardianId);
        }

        [TestMethod]
        public void ExistsReturnsBool()
        {
            //Arrange
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));

            //Act
            var result = controller.Exists(0, 1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckRelationReturnsGuardianWard()
        {
            //Arrange
            moq.Setup(m => m.Exists(It.IsAny<GuardianWard>())).Returns<GuardianWard>(g => Task.FromResult(g));
            long wardId = 3;
            GuardianWard guardian = new GuardianWard { WardId = wardId };

            //Act
            var result = (controller.Exists(wardId, guardian).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(GuardianWard));
            Assert.AreEqual(wardId, (result as GuardianWard)?.WardId);
        }

        [TestMethod]
        public void PostReturnsGuardianWard()
        {
            //Arrange
            moq.Setup(m => m.Post(It.IsAny<GuardianWard>())).Returns<GuardianWard>(g => Task.FromResult(g));

            //Act
            var result = (controller.Post(new GuardianWard()).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(GuardianWard));
        }

        [TestMethod]
        public void PutReturnsGuardianWard()
        {
            //Arrange
            moq.Setup(m => m.Put(It.IsAny<GuardianWard>())).Returns<GuardianWard>(g => Task.FromResult(g));
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));
            long wardId = 1;
            long guardianId = wardId + 1;

            //Act
            var result = (controller.Put(wardId, guardianId, new GuardianWard
            {
                WardId = wardId,
                GuardianId = guardianId
            }).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(GuardianWard));
        }

        [TestMethod]
        public void DeleteReturnsGuardianWard()
        {
            //Arrange
            moq.Setup(m => m.Delete(It.IsAny<object[]>())).Returns(Task.FromResult(new GuardianWard()));
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));
            long wardId = 1;
            long guardianId = wardId + 1;

            //Act
            var result = (controller.Delete(wardId, guardianId).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(GuardianWard));
        }
    }
}
