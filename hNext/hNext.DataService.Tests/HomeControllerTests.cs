using hNext.DataService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.DataService.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void HomeControllerReturnsSeverUp()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.AreEqual(result, "Service Up");
        }
    }
}
