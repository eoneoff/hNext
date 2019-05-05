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
    public class PhonesControllerTests
    {
        private Mock<IPhoneRepository> moq = new Mock<IPhoneRepository>();
        private PhonesController controller;

        public PhonesControllerTests()
        {
            controller = new PhonesController(moq.Object);
        }

        [TestMethod]
        public void GetReturnsListOfPhones()
        {
            //Arrange
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Phone>() as IEnumerable<Phone>));

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Phone>));
        }

        [TestMethod]
        public void GetIdReturnsPhone()
        {
            //Arrange
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Phone { Id = (long)id[0] }));
            long phoneId = 3;

            //Act
            var result = controller.Get(phoneId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Phone));
            Assert.AreEqual(phoneId, result.Id);
        }

        [TestMethod]
        public void ExistsReturnsPhone()
        {
            //Arrange
            moq.Setup(m => m.Exists(It.IsAny<string>())).Returns<string>(n => Task.FromResult(new Phone
            {
                Number = n
            }));
            string number = "test number";

            //Act
            var result = controller.Exists(number).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Phone));
            Assert.AreEqual(number, result.Number);
        }

        [TestMethod]
        public void BelongsToOthersReturnsBool()
        {
            //Arrange
            moq.Setup(m => m.BelongToOthers(It.IsAny<long>())).Returns(Task.FromResult(true));

            //Act
            var result = controller.BelongToOthers(0).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PostReturnsPhone()
        {
            //Arrange
            moq.Setup(m => m.Post(It.IsAny<Phone>())).Returns<Phone>(p => Task.FromResult(p));

            //Act
            var result = (controller.Post(new Phone()).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Phone));
        }

        [TestMethod]
        public void PutReturnsPhone()
        {
            //Arrange
            moq.Setup(m => m.Put(It.IsAny<Phone>())).Returns<Phone>(p => Task.FromResult(p));
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));

            //Act
            var result = (controller.Put(0, new Phone()).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Phone));
        }

        [TestMethod]
        public void DeleteReturnsPhone()
        {
            //Arrange
            moq.Setup(m => m.Delete(It.IsAny<object[]>())).Returns(Task.FromResult(new Phone()));

            //Act
            var result = (controller.Delete(0).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Phone));
        }
    }
}
