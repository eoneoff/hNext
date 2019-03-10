using hNext.WebClient.Components;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class MainViewViewComponentTests
    {
        [TestMethod]
        public void DefaultReturnsCorrectView()
        {
            //Arrange
            ApplicationViewModel model = new ApplicationViewModel();
            MainViewViewComponent component = new MainViewViewComponent();

            //Act
            var result = component.Invoke(model) as ViewViewComponentResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.ViewData.Model);
        }
    }
}
