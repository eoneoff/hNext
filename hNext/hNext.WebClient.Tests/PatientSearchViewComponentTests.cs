using hNext.IRepository;
using hNext.Model;
using hNext.WebClient.Components;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hNext.WebClient.Infrastructure;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class PatientSearchViewComponentTests
    {
        [TestMethod]
        public void DefaultReturnsView()
        {
            //Arrange
            HashSet<string> modules = new HashSet<string>();
            var moq = new Mock<IRepository<Region>>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(default(IEnumerable<Region>)));
            Components.PatientSearchViewComponent component = new Components.PatientSearchViewComponent(moq.Object);

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IViewComponentResult));
        }

        [TestMethod]
        public void DefaultReturnsCorrectModel()
        {
            //Arrange
            HashSet<string> modules = new HashSet<string>();
            var moq = new Mock<IRepository<Region>>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Region> { new Region(), new Region()} as IEnumerable<Region>));
            Components.PatientSearchViewComponent component = new Components.PatientSearchViewComponent(moq.Object);

            //Act
            var result = (component.InvokeAsync(modules).Result as ViewViewComponentResult).ViewData.Model as PatientSearchViewModel;

            //Assert
            Assert.IsInstanceOfType(result, typeof(PatientSearchViewModel));
            Assert.AreEqual(result.Regions.Count(), 2);
        }

        [TestMethod]
        public void DefaultPopulatesModules()
        {
            //Arrange
            HashSet<string> modules = new HashSet<string>();
            var moq = new Mock<IRepository<Region>>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(default(IEnumerable<Region>)));
            Components.PatientSearchViewComponent component = new PatientSearchViewComponent(moq.Object);

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            Assert.IsTrue(modules.Contains(nameof(PersonEditorViewComponent).VewComponentName()));
        }
    }
}
