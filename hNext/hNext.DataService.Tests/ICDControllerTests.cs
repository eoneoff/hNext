using hNext.DataService.Controllers;
using hNext.IRepository;
using hNext.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.DataService.Tests
{
    [TestClass]
    public class ICDControllerTests
    {
        private Mock<IICDRepository> repository = new Mock<IICDRepository>();
        private ICDController controller;

        public ICDControllerTests()
        {
            controller = new ICDController(repository.Object);
        }

        [TestMethod]
        public void GetReturnsListOfICDs()
        {
            //Arrange
            repository.Setup(r => r.Get()).ReturnsAsync(new List<ICD>() as IEnumerable<ICD>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ICD>));
        }

        [TestMethod]
        public void SearchReturnsICD()
        {
            //Arrange
            repository.Setup(r => r.Search(It.IsAny<ICD>())).ReturnsAsync(new ICD());

            //Act
            var result = controller.Search(new ICD()).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ICD));
        }
    }
}
