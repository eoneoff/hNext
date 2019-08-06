using Microsoft.Extensions.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using hNext.ResourceLibrary.Resources;
using hNext.IRepository;
using hNext.WebClient.Controllers;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;
using hNext.WebClient.Models;

namespace hNext.WebClient.Tests
{
    [TestClass]
    public class CaseHistoriesControllerTests
    {
        Mock<IStringLocalizer<Resources>> localizer = new Mock<IStringLocalizer<Resources>>();
        Mock<ICaseHistoryRepository> repository = new Mock<ICaseHistoryRepository>();
        CaseHistoriesController controller;

        public CaseHistoriesControllerTests()
        {
            controller = new CaseHistoriesController(localizer.Object, repository.Object);
        }

        [TestMethod]
        public void IndexReturnsView()
        {
            //Arrange
            repository.Setup(r => r.Info(It.IsAny<long>())).ReturnsAsync((long id) =>
            {
                return new CaseHistory { Id = id };
            });
            long caseHistoryId = 1;

            //Act
            var result = controller.Index(caseHistoryId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void IndexReturnsCorrrectModel()
        {
            //Arrange
            repository.Setup(r => r.Info(It.IsAny<long>())).ReturnsAsync((long id) =>
            {
                return new CaseHistory { Id = id };
            });
            long caseHistoryId = 1;

            //Act
            var result = (controller.Index(caseHistoryId).Result as ViewResult).Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CaseHistoryViewModel));
            Assert.AreEqual(caseHistoryId, (result as CaseHistoryViewModel)?.CaseHistory?.Id);
        }
    }
}
