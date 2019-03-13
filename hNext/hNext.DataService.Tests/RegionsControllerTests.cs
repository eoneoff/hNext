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
    public class RegionsControllerTests
    {
        [TestMethod]
        public void GetReturnsListOfRegions()
        {
            //Arrange
            var moq = new Mock<IRegionsRepository>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Region>() as IEnumerable<Region>));
            RegionsController controller = new RegionsController(moq.Object);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Region>));
        }

        [TestMethod]
        public void GetIdReturnsRegion()
        {
            //Arrange
            var moq = new Mock<IRegionsRepository>();
            moq.Setup(m => m.Get(It.IsAny<long>())).Returns<long>(id => Task.FromResult(new Region { Id = (int)id }));
            RegionsController controller = new RegionsController(moq.Object);
            int regionId = 3;

            //Act
            var result = controller.Get(regionId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Region));
            Assert.AreEqual(regionId, result.Id);
        }

        [TestMethod]
        public void GetDistrictsReturnsListOfDistricts()
        {
            //Arrange
            var moq = new Mock<IRegionsRepository>();
            moq.Setup(m => m.GetDistricts(It.IsAny<int>())).Returns(Task.FromResult(new List<District>() as IEnumerable<District>));
            RegionsController controller = new RegionsController(moq.Object);

            //Act
            var result = controller.GetDistricts(3).Result;

            //Arrange
            Assert.IsInstanceOfType(result, typeof(IEnumerable<District>));
        }

        [TestMethod]
        public void GetCitiesReturnsListOfCities()
        {
            //Arrange
            var moq = new Mock<IRegionsRepository>();
            moq.Setup(m => m.GetCities(It.IsAny<int>())).Returns(Task.FromResult(new List<City>() as IEnumerable<City>));
            RegionsController controller = new RegionsController(moq.Object);

            //Act
            var result = controller.GetCities(3).Result;

            //Arrange
            Assert.IsInstanceOfType(result, typeof(IEnumerable<City>));
        }
    }
}
