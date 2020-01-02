using hNext.DataService.Controllers;
using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.DataService.Tests
{
    [TestClass]
    public class RecordTemplatesControllerTests
    {
        private Mock<IPoster<RecordTemplate>> repository = new Mock<IPoster<RecordTemplate>>();
        private RecordTemplatesController controller;

        public RecordTemplatesControllerTests()
        {
            controller = new RecordTemplatesController(repository.Object);
        }

        [TestMethod]
        public void GetReturnsListOfRecordTemplates()
        {
            //Arrange
            repository.Setup(m => m.Get()).ReturnsAsync(new List<RecordTemplate>() as IEnumerable<RecordTemplate>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<RecordTemplate>));
        }

        [TestMethod]
        public void GetIdReturnsRecordTemplate()
        {
            //Arrange
            repository.Setup(m => m.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new RecordTemplate
                {
                    Id = (int)id[0]
                };
            });

            int templateId = 1;

            //Act
            var result = controller.Get(templateId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RecordTemplate));
            Assert.AreEqual(templateId, result.Id);
        }

        [TestMethod]
        public void PostReturnsRecordTemplate()
        {
            //Arrange
            repository.Setup(r => r.Post(It.IsAny<RecordTemplate>())).ReturnsAsync((RecordTemplate t) => { return t; });

            //Act
            var result = (controller.Post(new RecordTemplate()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RecordTemplate));
        }
    }
}
