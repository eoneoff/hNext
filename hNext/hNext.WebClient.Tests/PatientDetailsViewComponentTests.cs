using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class PatientDetailsViewComponentTests
    {
        [TestMethod]
        public void DefaultReturnsViewView()
        {
            //Arrange
            Components.PatientDetailsViewComponent component = new Components.PatientDetailsViewComponent();

            //Act
            var result = component.Invoke();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewViewComponentResult));
        }
    }
}
