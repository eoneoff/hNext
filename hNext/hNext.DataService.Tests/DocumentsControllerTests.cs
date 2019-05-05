using hNext.DataService.Controllers;
using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.DataService.Tests
{
    [TestClass]
    public class DocumentsControllerTests
    {
        [TestMethod]
        public void GetReturnsListOfDocuments()
        {
            //Arrange
            var moq = new Mock<IDocumentsRepository>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Document>() as IEnumerable<Document>));
            DocumentsController controller = new DocumentsController(moq.Object);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Document>));
        }

        [TestMethod]
        public void GetIdReurnsDocument()
        {
            //Arrange
            var moq = new Mock<IDocumentsRepository>();
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id =>
            Task.FromResult(new Document { Id = (long)id[0] }));
            DocumentsController controller = new DocumentsController(moq.Object);
            long documentId = 2;

            //Act
            var result = controller.Get(documentId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Document));
            Assert.AreEqual(result.Id, documentId);
        }

        [TestMethod]
        public void ExistsReturnsBool()
        {
            //Arrange
            var moq = new Mock<IDocumentsRepository>();
            moq.Setup(m => m.Exists(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            DocumentsController controller = new DocumentsController(moq.Object);

            //Act
            var result = controller.Exists(0, string.Empty).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PostReturnsDocument()
        {
            //Arrange
            var moq = new Mock<IDocumentsRepository>();
            moq.Setup(m => m.Post(It.IsAny<Document>())).Returns<Document>(d => Task.FromResult(d));
            DocumentsController controller = new DocumentsController(moq.Object);

            //Act
            var result = (controller.Post(new Document()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Document));
        }

        [TestMethod]
        public void PutReturnsDocument()
        {
            //Arrange
            var moq = new Mock<IDocumentsRepository>();
            moq.Setup(m => m.Put(It.IsAny<Document>())).Returns<Document>(d => Task.FromResult(d));
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));
            DocumentsController controller = new DocumentsController(moq.Object);

            //Act
            var result = (controller.Put(0, new Document()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Document));
        }

        [TestMethod]
        public void DeleteReturnsDocument()
        {
            //Arrange
            var moq = new Mock<IDocumentsRepository>();
            moq.Setup(m => m.Delete(It.IsAny<object[]>())).Returns(Task.FromResult(new Document()));
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));
            DocumentsController controller = new DocumentsController(moq.Object);

            //Act
            var result = (controller.Delete(0).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Document));
        }
    }
}
