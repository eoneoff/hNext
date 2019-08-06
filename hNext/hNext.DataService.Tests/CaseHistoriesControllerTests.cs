﻿using hNext.DataService.Controllers;
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
    public class CaseHistoriesControllerTests
    {
        private Mock<ICaseHistoryRepository> repository = new Mock<ICaseHistoryRepository>();
        private Mock<IRepository<CaseHistoryDiagnosys>> diagnosesRepository = new Mock<IRepository<CaseHistoryDiagnosys>>();
        private CaseHistoriesController controller;

        public CaseHistoriesControllerTests()
        {
            controller = new CaseHistoriesController(repository.Object, diagnosesRepository.Object);
        }

        [TestMethod]
        public void GetReturnsListOfCaseHistories()
        {
            //Arrange
            repository.Setup(r => r.Get()).ReturnsAsync(new List<CaseHistory>() as IEnumerable<CaseHistory>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<CaseHistory>));
        }

        [TestMethod]
        public void GetIdReturndsCaseHistory()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new CaseHistory
                {
                    Id = (long)id[0]
                };
            });
            long historyId = 1;

            //Act
            var result = controller.Get(historyId).Result;

            Assert.IsInstanceOfType(result, typeof(CaseHistory));
            Assert.AreEqual(historyId, result?.Id);
        }

        [TestMethod]
        public void InfoReturnsCaseHistory()
        {
            //Arrange
            repository.Setup(r => r.Info(It.IsAny<long>())).ReturnsAsync((long id) =>
            {
                return new CaseHistory { Id = id };
            });
            long caseHistoryId = 1;

            //Act
            var result = controller.Info(caseHistoryId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CaseHistory));
            Assert.AreEqual(caseHistoryId, result.Id);
        }

        [TestMethod]
        public void PostReturnsCaseHistory()
        {
            //Arrange
            repository.Setup(r => r.Post(It.IsAny<CaseHistory>())).ReturnsAsync((CaseHistory h) => { return h; });

            //Act
            var result = (controller.Post(new CaseHistory()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CaseHistory));
        }

        [TestMethod]
        public void PutReturnsCaseHistory()
        {
            //Arrange
            repository.Setup(r => r.Put(It.IsAny<CaseHistory>())).ReturnsAsync((CaseHistory h) => { return h; });
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            long historyId = 1;

            //Act
            var result = (controller.Put(historyId, new CaseHistory { Id = historyId }).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CaseHistory));
            Assert.AreEqual(historyId, (result as CaseHistory)?.Id);
        }

        [TestMethod]
        public void AddDiagnosysReturnsCaseHistoryDiagnosys()
        {
            //Arrange
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            diagnosesRepository.Setup(d => d.Exists(It.IsAny<object[]>())).ReturnsAsync(false);
            diagnosesRepository.Setup(d => d.Post(It.IsAny<CaseHistoryDiagnosys>()))
                .ReturnsAsync((CaseHistoryDiagnosys d) => { return d; });
            long caseHistoryId = 1;

            //Act
            var result = (controller.AddDiagnosys(caseHistoryId, new CaseHistoryDiagnosys()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CaseHistoryDiagnosys));
            Assert.AreEqual(caseHistoryId, (result as CaseHistoryDiagnosys)?.CaseHistoryId);
        }

        [TestMethod]
        public void RemoveDiagnosysReturnsCaseHistoryDiagnosys()
        {
            //Arrange
            diagnosesRepository.Setup(dr => dr.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            diagnosesRepository.Setup(dr => dr.Delete(It.IsAny<object[]>())).ReturnsAsync(new CaseHistoryDiagnosys());

            //Act
            var result = (controller.RemoveDiagnosys(1, 1).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CaseHistoryDiagnosys));
        }
    }
}
