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
    public class PatientAdditionalDataViewComponentTests
    {
        PatientAdditionalDataViewComponent component = new PatientAdditionalDataViewComponent();
        UniqueList<string> modules = new UniqueList<string>();

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
        public void InvokeAddsNecessaryComponents()
        {
            //Arrange

            //Act
            var result = component.Invoke(modules);

            //Assert
            Assert.IsTrue(modules.Contains(nameof(PhonesListViewComponent).ViewComponentName()));
            Assert.IsTrue(modules.Contains(nameof(EmailsListViewComponent).ViewComponentName()));
            Assert.IsTrue(modules.Contains(nameof(DocumentsListViewComponent).ViewComponentName()));
            Assert.IsTrue(modules.Contains(nameof(GuardiansListViewComponent).ViewComponentName()));
        }
    }
}
