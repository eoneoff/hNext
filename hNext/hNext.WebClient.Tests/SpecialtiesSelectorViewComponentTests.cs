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
    public class SpecialtiesSelectorViewComponentTests
    {
        private SpecialtiesSelectorViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();
        private Mock<IGetter<Specialty>> repository = new Mock<IGetter<Specialty>>();

        public SpecialtiesSelectorViewComponentTests()
        {
            component = new SpecialtiesSelectorViewComponent(repository.Object);
        }

        [TestMethod]
        public void InvokeReturnsVIew()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewViewComponentResult));
        }

        [TestMethod]
        public void InvokeReturnsCorrectModel()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            Assert.IsInstanceOfType((result as ViewViewComponentResult).ViewData.Model, typeof(SpecialtiesSelectorViewModel));
        }

        [TestMethod]
        public void InvokeAddNecessaryModules()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            CollectionAssert.Contains(modules, nameof(ConfirmationDialogViewComponent).ViewComponentName());
        }
    }
}
