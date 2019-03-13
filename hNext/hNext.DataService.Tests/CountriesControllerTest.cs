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
    public class CountriesControllerTest
    {
        [TestMethod]
        public void GetReturnsListOfCountries()
        {
            //Arrange
            var moq = new Mock<ICountryRepository>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Country>() as IEnumerable<Country>));
            CountriesController controller = new CountriesController(moq.Object);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Country>));
        }

        [TestMethod]
        public void GetIdReturnsCountry()
        {
            //Arrange
            var moq = new Mock<ICountryRepository>();
            moq.Setup(m => m.Get(It.IsAny<long>())).Returns<long>(id => Task.FromResult(new Country { Id = (int)id }));
            CountriesController controller = new CountriesController(moq.Object);
            int districtId = 3;

            //Act
            var result = controller.Get(districtId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Country));
            Assert.AreEqual(districtId, result.Id);
        }

        [TestMethod]
        public void GetRegionsReturnsListOfRegions()
        {
            //Arrange
            var moq = new Mock<ICountryRepository>();
            moq.Setup(m => m.GetRegions(It.IsAny<int>())).Returns(Task.FromResult(new List<Region>() as IEnumerable<Region>));
            CountriesController controller = new CountriesController(moq.Object);

            //Act
            var result = controller.GetRegions(3).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Region>));
        }

        [TestMethod]
        public void GetCitiesReturnsListOfCities()
        {
            //Arrange
            var moq = new Mock<ICountryRepository>();
            moq.Setup(m => m.GetCities(It.IsAny<int>())).Returns(Task.FromResult(new List<City>() as IEnumerable<City>));
            CountriesController controller = new CountriesController(moq.Object);

            //Act
            var result = controller.GetCities(3).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<City>));
        }
    }
}
