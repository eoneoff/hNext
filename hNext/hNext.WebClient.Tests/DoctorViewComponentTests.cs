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
    public class DoctorViewComponentTests
    {
        private DoctorViewComponent component;
        private Mock<IGetter<Specialty>> specialties = new Mock<IGetter<Specialty>>();
        private UniqueList<string> modules = new UniqueList<string>();

        public DoctorViewComponentTests()
        {
            component = new DoctorViewComponent(specialties.Object);
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
            var result = component.InvokeAsync(modules).Result as ViewViewComponentResult;

            //Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(DoctorViewModel));
        }

        [TestMethod]
        public void InvokeAddsNecessaryModules()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            CollectionAssert.Contains(modules, nameof(PersonEditorViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(SpecialtiesListViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(DoctorSpecialtyEditorViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(DoctorPositionListViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(DiplomaListViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(ConfirmationDialogViewComponent).ViewComponentName());
        }
    }
}
