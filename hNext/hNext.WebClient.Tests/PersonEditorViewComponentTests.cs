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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class PersonEditorViewComponentTests
    {
        [TestMethod]
        public void InvokeReturnsCorrectModel()
        {
            //Arrange
            List<string> modules = new List<string>();
            var cRep = new Mock<ICountryRepository>();
            cRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<Country>() as IEnumerable<Country>));
            var gRep = new Mock<IRepository<Gender>>();
            gRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<Gender>() as IEnumerable<Gender>));
            var cTRep = new Mock<IRepository<CityType>>();
            cTRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<CityType>() as IEnumerable<CityType>));
            var sTRep = new Mock<IRepository<StreetType>>();
            sTRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<StreetType>() as IEnumerable<StreetType>));

            PersonEditorViewComponent component = new PersonEditorViewComponent(cRep.Object, gRep.Object, sTRep.Object, cTRep.Object);

            //Act
            var result = component.InvokeAsync(modules).Result as ViewViewComponentResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewViewComponentResult));
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(PersonEditorViewModel));
        }

        [TestMethod]
        public void InvokeSetsCollertions()
        {
            //Arrange
            HashSet<string> modules = new HashSet<string>();
            var cRep = new Mock<ICountryRepository>();
            cRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<Country>
            {
                new Country(),
                new Country(),
                new Country()
            }
            as IEnumerable<Country>));
            var gRep = new Mock<IRepository<Gender>>();
            gRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<Gender>
            {
                new Gender(),
                new Gender()
            } as IEnumerable<Gender>));
            var cTRep = new Mock<IRepository<CityType>>();
            cTRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<CityType>
            {
                new CityType(),
                new CityType(),
                new CityType()
            } as IEnumerable<CityType>));
            var sTRep = new Mock<IRepository<StreetType>>();
            sTRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<StreetType>
            {
                new StreetType(),
                new StreetType(),
                new StreetType()
            } as IEnumerable<StreetType>));

            Components.PersonEditorViewComponent component = new Components.PersonEditorViewComponent(cRep.Object, gRep.Object, sTRep.Object, cTRep.Object);

            //Act
            var result = (component.InvokeAsync(modules).Result as ViewViewComponentResult).ViewData.Model as PersonEditorViewModel;

            //Arrange
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Countries.Count(), 3);
            Assert.AreEqual(result.Genders.Count(), 2);
            Assert.AreEqual(result.CityTypes.Count(), 3);
            Assert.AreEqual(result.StreetTypes.Count(), 3);
        }

        [TestMethod]
        public void InvokePopulatesModulesSet()
        {
            //Arrange
            HashSet<string> modules = new HashSet<string>();
            var cRep = new Mock<ICountryRepository>();
            cRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<Country>() as IEnumerable<Country>));
            var gRep = new Mock<IRepository<Gender>>();
            gRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<Gender>() as IEnumerable<Gender>));
            var cTRep = new Mock<IRepository<CityType>>();
            cTRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<CityType>() as IEnumerable<CityType>));
            var sTRep = new Mock<IRepository<StreetType>>();
            sTRep.Setup(r => r.Get()).Returns(Task.FromResult(new List<StreetType>() as IEnumerable<StreetType>));

            Components.PersonEditorViewComponent component = new Components.PersonEditorViewComponent(cRep.Object, gRep.Object, sTRep.Object, cTRep.Object);

            //Act
            var result = component.InvokeAsync(modules);

            //Assert
            Assert.IsTrue(modules.Contains(nameof(ConfirmationDialogViewComponent).ViewComponentName()));
        }
    }
}
