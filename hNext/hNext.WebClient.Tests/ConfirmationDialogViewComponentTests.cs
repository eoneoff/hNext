using hNext.WebClient.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class ConfirmationDialogViewComponentTests
    {
        [TestMethod]
        public void IvokeReturnsCorrectResult()
        {
            //Arrange
            ConfirmationDialogViewComponent component = new ConfirmationDialogViewComponent();

            //Act
            var result = component.Invoke() as ViewViewComponentResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
