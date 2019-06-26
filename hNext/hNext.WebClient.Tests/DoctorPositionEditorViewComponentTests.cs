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
    public class DoctorPositionEditorViewComponentTests
    {
        private Mock<IGetter<Specialty>> specialties = new Mock<IGetter<Specialty>>();
        private Mock<IGetter<Position>> positions = new Mock<IGetter<Position>>();
        private DoctorPositionEditorViewComponent component;
        UniqueList<string> modules = new UniqueList<string>();

        public DoctorPositionEditorViewComponentTests()
        {
            component = new DoctorPositionEditorViewComponent(specialties.Object, positions.Object);
            specialties.Setup(s => s.Get()).ReturnsAsync(new List<Specialty>() as IEnumerable<Specialty>);
            positions.Setup(p => p.Get()).ReturnsAsync(new List<Position>() as IEnumerable<Position>);
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

            //Arrange
            Assert.IsInstanceOfType(result, typeof(DoctorPositionEditorViewModel));
        }

        [TestMethod]
        public void InvokeAddsNecessarymodlues()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            CollectionAssert.Contains(modules, nameof(ConfirmationDialogViewComponent).ViewComponentName());
        }
    }
}
