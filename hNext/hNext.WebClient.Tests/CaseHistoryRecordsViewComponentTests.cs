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
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class CaseHistoryRecordsViewComponentTests
    {
        Mock<IRepository<RecordTemplate>> _repository = new Mock<IRepository<RecordTemplate>>();
        private CaseHistoryRecordsViewComponent component;
        private UniqueList<string> modules = new UniqueList<string>();

        public CaseHistoryRecordsViewComponentTests()
        {
            _repository.Setup(r => r.Get()).ReturnsAsync(new List<RecordTemplate>() as IEnumerable<RecordTemplate>);
            component = new CaseHistoryRecordsViewComponent(_repository.Object);
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
            Assert.IsInstanceOfType(result, typeof(IEnumerable<RecordTemplate>));
        }

        [TestMethod]
        public void InvokeAddsNecessaryModules()
        {
            //Arrange
            var mods = new List<string>
            {
                nameof(RecordViewComponent).ViewComponentName(),
                nameof(CaseHistoryDiagnosysEditorViewComponent).ViewComponentName()
            };

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            CollectionAssert.AreEquivalent(mods, modules);
        }
    }
}
