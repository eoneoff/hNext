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
    public class SpecialtiesControllerTests
    {
        private Mock<IGetter<Specialty>> getter = new Mock<IGetter<Specialty>>();
        private SpecialtiesController controller;

        public SpecialtiesControllerTests()
        {
            controller = new SpecialtiesController(getter.Object);
        }

        [TestMethod]
        public void GetReturnsListOfSpecialties()
        {
            //Arrange
            getter.Setup(g => g.Get()).ReturnsAsync(new List<Specialty>() as IEnumerable<Specialty>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Specialty>));
        }
    }
}
