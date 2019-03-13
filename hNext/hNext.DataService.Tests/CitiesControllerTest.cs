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
    public class CitiesControllerTest
    {
        [TestMethod]
        public void GetReturnsListOfCities()
        {
            //Arrange
            var moq = new Mock<ICityRepository>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<City>() as IEnumerable<City>));
            CitiesController controller = new CitiesController(moq.Object);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<City>));
        }

        [TestMethod]
        public void GetIdReturnsCity()
        {
            //Arrange
            var moq = new Mock<ICityRepository>();
            moq.Setup(m => m.Get(It.IsAny<long>())).Returns<long>(id => Task.FromResult(new City { Id = (int)id }));
            CitiesController controller = new CitiesController(moq.Object);
            int districtId = 3;

            //Act
            var result = controller.Get(districtId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(City));
            Assert.AreEqual(districtId, result.Id);
        }

        [TestMethod]
        public void GetStreetsReturnsListOfStreets()
        {
            //Arrange
            var moq = new Mock<ICityRepository>();
            moq.Setup(m => m.GetStreets(It.IsAny<int>())).Returns(Task.FromResult(new List<Street>() as IEnumerable<Street>));
            CitiesController controller = new CitiesController(moq.Object);

            //Act
            var result = controller.GetStreets(3).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Street>));
        }
    }
}
