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
    public class DrugsControllerTests
    {
        private Mock<IDrugRepository> repository = new Mock<IDrugRepository>();

        private DrugsController controller;

        public DrugsControllerTests()
        {
            controller = new DrugsController(repository.Object);
        }

        [TestMethod]
        public void GetReturnsListOfDrugs()
        {
            //Arrange
            repository.Setup(r => r.Get()).ReturnsAsync(new List<Drug>() as IEnumerable<Drug>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Drug>));
        }

        [TestMethod]
        public void SearchReturnsListOfDrugs()
        {
            //Arrange
            repository.Setup(r => r.Search(It.IsAny<string>())).ReturnsAsync(new List<Drug>() as IEnumerable<Drug>);

            //Act
            var result = controller.Search("").Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Drug>));
        }
    }
}