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
    public class DistrictsControllerTests
    {
        [TestMethod]
        public void GetReturnsListOfDistricts()
        {
            //Arrange
            var moq = new Mock<IDistrictsRepository>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<District>() as IEnumerable<District>));
            DistrictsController controller = new DistrictsController(moq.Object);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<District>));
        }

        [TestMethod]
        public void GetIdReturnsDistrict()
        {
            //Arrange
            var moq = new Mock<IDistrictsRepository>();
            moq.Setup(m => m.Get(It.IsAny<int>())).Returns<int>(id => Task.FromResult(new District { Id = id }));
            DistrictsController controller = new DistrictsController(moq.Object);
            int districtId = 3;

            //Act
            var result = controller.Get(districtId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(District));
            Assert.AreEqual(districtId, result.Id);
        }

        [TestMethod]
        public void GetCitiesReturnListOfCities()
        {
            //Arrange
            var moq = new Mock<IDistrictsRepository>();
            moq.Setup(m => m.GetCities(It.IsAny<int>())).Returns(Task.FromResult(new List<City>() as IEnumerable<City>));
            DistrictsController controller = new DistrictsController(moq.Object);

            //Act
            var result = controller.GetCities(3).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<City>));
        }
    }
}
