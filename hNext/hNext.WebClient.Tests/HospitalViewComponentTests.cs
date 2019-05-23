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
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class HospitalViewComponentTests
    {
        private Mock<ICountryRepository> countries = new Mock<ICountryRepository>();
        private Mock<IRepository<PropertyType>> propertyTypes = new Mock<IRepository<PropertyType>>();
        private Mock<IRepository<HospitalType>> hospitalTypes = new Mock<IRepository<HospitalType>>();
        private HospitalsViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public HospitalViewComponentTests()
        {
            component = new HospitalsViewComponent(countries.Object, hospitalTypes.Object, propertyTypes.Object);
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
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(HospitalsViewModel));
        }

        [TestMethod]
        public void InvokeAddsNecessaryComponents()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            CollectionAssert.Contains(modules, nameof(PhonesListViewComponent).ViewComponentName());
            CollectionAssert.Contains(modules, nameof(EmailsListViewComponent).ViewComponentName());
        }
    }
}
