using hNext.Infrastructure;
using hNext.IRepository;
using hNext.Model;
using hNext.WebClient.Components;
using hNext.WebClient.Infrastructure;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class PhoneEditorViewComponentTests
    {
        private Mock<IGetter<PhoneType>> repository = new Mock<IGetter<PhoneType>>();
        private PhoneEditorViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public PhoneEditorViewComponentTests()
        {
            repository.Setup(r => r.Get()).ReturnsAsync(new List<PhoneType>() as IEnumerable<PhoneType>);
            component = new PhoneEditorViewComponent(repository.Object);
        }

        [TestMethod]
        public void InvokeReturnsView()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewViewComponentResult));
        }

        [TestMethod]
        public void InvokeReturnsModel()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result as ViewViewComponentResult;

            //Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(PhoneEditorViewModel));
        }

        [TestMethod]
        public void InvokeAddsNecessaryModules()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            Assert.IsTrue(modules.Contains(nameof(ConfirmationDialogViewComponent).ViewComponentName()));
        }
    }
}
