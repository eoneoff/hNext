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
    public class DiplomaEditorViewComponentTests
    {
        private DiplomaEditorViewComponent component = new DiplomaEditorViewComponent();
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
        public void InvokeReturnsCorrectModel()
        {
            //Arrange

            //Act
            var result = (component.Invoke(modules) as ViewViewComponentResult).ViewData.Model;

            //
            Assert.IsInstanceOfType(result, typeof(Diploma));
        }

        [TestMethod]
        public void InovkeAddsNecessaryModules()
        {
            //Arrange

            //Act
            var result = component.Invoke(modules);

            //Assert
            CollectionAssert.Contains(modules, nameof(ConfirmationDialogViewComponent).ViewComponentName());
        }
    }
}
