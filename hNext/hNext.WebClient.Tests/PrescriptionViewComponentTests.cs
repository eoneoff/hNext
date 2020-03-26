using hNext.Infrastructure;
using hNext.IRepository;
using hNext.Model;
using hNext.WebClient.Components;
using hNext.WebClient.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class PrescriptionViewComonentTests
    {
        private PrescriptionViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public PrescriptionViewComonentTests()
        {
            component = new PrescriptionViewComponent();
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
    }
}