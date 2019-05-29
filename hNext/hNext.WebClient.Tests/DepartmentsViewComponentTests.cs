using hNext.Infrastructure;
using hNext.WebClient.Components;
using hNext.WebClient.Infrastructure;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class DepartmentsViewComponentTests
    {
        private DepartmentsViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public DepartmentsViewComponentTests()
        {
            component = new DepartmentsViewComponent();
        }

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
            var result = component.Invoke(modules) as ViewViewComponentResult;

            //Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(DepartmentsViewModel));
        }

        [TestMethod]
        public void InvokeAddsNecessaryComponents()
        {
            //Arrange

            //Act
            var result = component.Invoke(modules);

            //Assert
            CollectionAssert.Contains(modules, nameof(PhonesListViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(EmailsListViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(ConfirmationDialogViewComponent).ViewComponentName());
        }
    }
}
