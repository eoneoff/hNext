using hNext.Infrastructure;
using hNext.Model;
using hNext.WebClient.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class RecordViewComponentTests
    {
        private RecordViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public RecordViewComponentTests()
        {
            component = new RecordViewComponent();
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
            var result = (component.Invoke(modules) as ViewViewComponentResult).ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Record));
        }
    }
}
