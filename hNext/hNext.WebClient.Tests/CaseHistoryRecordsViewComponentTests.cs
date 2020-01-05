using hNext.Infrastructure;
using hNext.WebClient.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class CaseHistoryRecordsViewComponentTests
    {
        private CaseHistoryRecordsViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public CaseHistoryRecordsViewComponentTests()
        {
            component = new CaseHistoryRecordsViewComponent();
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
