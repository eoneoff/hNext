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
    public class EmailsControllerTests
    {
        private Mock<IEmailRepository> moq;
        private EmailsController controller;

        public EmailsControllerTests()
        {
            moq = new Mock<IEmailRepository>();
            controller = new EmailsController(moq.Object);
        }

        [TestMethod]
        public void GetReturnsListOfEmails()
        {
            //Arrange
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Email>() as IEnumerable<Email>));

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Email>));
        }

        [TestMethod]
        public void GetIdReturnsEmail()
        {
            //Arrange
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Email { Id = (long)id[0] }));
            long emailId = 1;

            //Act
            var result = controller.Get(emailId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Email));
            Assert.AreEqual(emailId, result.Id);
        }

        [TestMethod]
        public void ExistsRetunrsEmail()
        {
            //Arrange
            moq.Setup(m => m.Exists(It.IsAny<string>())).Returns<string>(a => Task.FromResult(new Email { Address = a}));
            string address = "test address";

            //Act
            var result = controller.Exists(address).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Email));
            Assert.AreEqual(address, result.Address);
        }

        [TestMethod]
        public void BelongToOthersReturnsBool()
        {
            //Arrange
            moq.Setup(m => m.BelongToOthers(It.IsAny<long>())).Returns(Task.FromResult(true));

            //Act
            var result = controller.BelongToOthers(0).Result;

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PostReturnsEmail()
        {
            //Arrange
            moq.Setup(m => m.Post(It.IsAny<Email>())).Returns<Email>(e => Task.FromResult(e));
            Email email = new Email();

            //Act
            var result = (controller.Post(email).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Email));
        }

        [TestMethod]
        public void PutReturnsEmail()
        {
            //Arrange
            moq.Setup(m => m.Put(It.IsAny<Email>())).Returns(Task.FromResult(new Email()));
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));

            //Act
            var result = (controller.Put(0, new Email()).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Email));
        }

        [TestMethod]
        public void DeleteReturnsEmail()
        {
            //Arrange
            moq.Setup(m => m.Delete(It.IsAny<object[]>())).Returns(Task.FromResult(new Email()));

            //Act
            var result = (controller.Delete(0).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Email));
        }
    }
}
