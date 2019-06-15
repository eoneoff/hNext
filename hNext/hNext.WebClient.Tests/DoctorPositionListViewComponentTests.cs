using hNext.Infrastructure;
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
    public class DoctorPositionListViewComponentTests
    {
        private DoctorPositionListViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public DoctorPositionListViewComponentTests()
        {
            component = new DoctorPositionListViewComponent();
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
        public void InvokeAddNecessaryModules()
        {
            //Arrange

            //Act
            var result = component.Invoke(modules);

            //Assert
            CollectionAssert.Contains(modules, nameof(ConfirmationDialogViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(DoctorPositionEditorViewComponent).ViewComponentName());
        }
    }
}
