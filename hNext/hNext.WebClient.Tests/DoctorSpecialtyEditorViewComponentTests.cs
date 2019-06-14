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
    public class DoctorSpecialtyEditorViewComponentTests
    {
        private Mock<IGetter<Specialty>> specialties = new Mock<IGetter<Specialty>>();
        private UniqueList<string> modules = new UniqueList<string>();
        private DoctorSpecialtyEditorViewComponent component;

        public DoctorSpecialtyEditorViewComponentTests()
        {
            component = new DoctorSpecialtyEditorViewComponent(specialties.Object);
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
            Assert.IsInstanceOfType(result, typeof(DoctorSpecialtyEditorViewModel));
        }

        [TestMethod]
        public void InvokeAddsNecessaryComponents()
        {
            //Arrange

            //Act
            component.InvokeAsync(modules);

            //Assert
            CollectionAssert.Contains(modules, nameof(ConfirmationDialogViewComponent).ViewComponentName());
        }
    }
}
