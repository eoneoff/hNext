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
    public class RecordsViewComponentTests
    {
        private RecordsViewComponent component = new RecordsViewComponent();
        private UniqueList<string> modules = new UniqueList<string>();

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
        public void InvokeAddsNecessaryModules()
        {
            //Arrange
            var mod = new List<string>
            {
                nameof(RecordTemplateEditorViewComponent).ViewComponentName()
            };

            //Act
            var result = component.Invoke(modules);

            //Assert
            CollectionAssert.AreEquivalent(mod, modules);
        }
    }
}
