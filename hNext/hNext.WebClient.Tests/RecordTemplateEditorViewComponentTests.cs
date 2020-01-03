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
    public class RecordTemplateEditorViewComponentTests
    {
        private Mock<IGetter<Hospital>> getter = new Mock<IGetter<Hospital>>();
        private RecordTemplateEditorViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public RecordTemplateEditorViewComponentTests()
        {
            component = new RecordTemplateEditorViewComponent(getter.Object);
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
        public void InvokeReturnsCorrectModel()
        {
            //Arrange

            //Act
            var result = (component.InvokeAsync(modules).Result as ViewViewComponentResult).ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RecordTemplateEditorViewModel));
        }

        [TestMethod]
        public void InvokeAddsNecessaryModules()
        {
            //Arrange
            var mods = new List<string>
            {
                nameof(RecordFieldTemplateEditorViewComponent).ViewComponentName(),
                nameof(ConfirmationDialogViewComponent).ViewComponentName()
            };

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            CollectionAssert.AreEquivalent(mods, modules);
        }
    }
}
