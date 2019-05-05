using System;
using System.Collections.Generic;
using System.Text;
using hNext.Infrastructure;
using hNext.WebClient.Components;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class PhonesListViewComponentTests
    {
        private PhonesListViewComponent component = new PhonesListViewComponent();
        private UniqueList<string> modules = new UniqueList<string>();

        [TestMethod]
        public void InvokeReturnsView()
        {
            //Arrange

            //Act
            var result = component.Invoke(modules);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewViewComponentResult));
        }

        [TestMethod]
        public void InvokeAddsNecessaryModules()
        {
            //Arrange

            //Act
            var result = component.Invoke(modules);

            //Assert
            Assert.IsTrue(modules.Contains(nameof(ConfirmationDialogViewComponent).ViewComponentName()));
            Assert.IsTrue(modules.Contains(nameof(PhoneEditorViewComponent).ViewComponentName()));
        }
    }
}
