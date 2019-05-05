using hNext.DbAccessMSSQLCore;
using hNext.Infrastructure;
using hNext.IRepository;
using hNext.Model;
using hNext.WebClient.Components;
using hNext.WebClient.Infrastructure;
using hNext.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class DocumentEditorVewComponentTests
    {
        private Mock<IRepository<DocumentType>> repository = new Mock<IRepository<DocumentType>>();
        private DocumentEditorViewComponent component;
        public DocumentEditorVewComponentTests()
        {
            component = new DocumentEditorViewComponent(repository.Object);
            repository.Setup(r => r.Get()).ReturnsAsync(new List<DocumentType>() as IEnumerable<DocumentType>);
        }

        [TestMethod]
        public void IvokeReturnsCorrectResult()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(new UniqueList<string>()).Result as ViewViewComponentResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void InvokeReturnsCorrectModel()
        {
            //Arrange

            //Act
            var result = component.InvokeAsync(new UniqueList<string>()).Result as ViewViewComponentResult;

            //Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(DocumentEditorViewModel));
        }

        [TestMethod]
        public void IvokeAddsRequiredComponents()
        {
            //Arrange
            UniqueList<string> modules = new UniqueList<string>();

            //Act
            var result = component.InvokeAsync(modules).Result;

            //Assert
            Assert.IsTrue(modules.Contains(nameof(ConfirmationDialogViewComponent).ViewComponentName()));
        }
    }
}
