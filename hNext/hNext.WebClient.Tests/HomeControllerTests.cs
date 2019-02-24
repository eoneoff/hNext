using hNext.ResourceLibrary.Resources;
using hNext.WebClient.Controllers;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void HomeControllerReturnsCorrectResult()
        {
            //Arrange
            var moq = new Mock<IStringLocalizer<Resources>>();
            HomeController controller = new HomeController(moq.Object);

            //Act
            var result = controller.Index() as IActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType((result as ViewResult)?.Model, typeof(ApplicationViewModel));
        }
    }
}
