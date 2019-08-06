using hNext.Infrastructure;
using hNext.Model;
using hNext.WebClient.Components;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class DiagnosysEditorViewComponentTests
    {
        private DiagnosysEditorViewComponent component = new DiagnosysEditorViewComponent();
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
        public void InvokeReturnsCorrrectModel()
        {
            //Arrange

            //Act
            var result = (component.Invoke(modules) as ViewViewComponentResult).ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Diagnosys));
        }

        [TestMethod]
        public void InvokeAddsNecessaryModules()
        {
            //Arrange
            var mod = new List<string>
            {
                nameof(ConfirmationDialogViewComponent).ViewComponentName(),
                nameof(ICDReferenceViewComponent).ViewComponentName()
            };

            //Act
            var result = component.Invoke(modules);

            //Assert
            CollectionAssert.AreEquivalent(mod, modules);
        }
    }
}
