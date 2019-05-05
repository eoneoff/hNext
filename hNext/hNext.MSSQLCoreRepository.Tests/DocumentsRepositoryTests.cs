using hNext.DbAccessMSSQLCore;
using hNext.IRepository;
using hNext.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hNext.MSSQLCoreRepository.Tests
{
    [TestClass]
    public class DocumentsRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void ExistsReturnsCorrectly()
        {
            //Arrange
            int documentTypeId = 1;
            string number = "test";
            var documents = new List<Document>
            {
                new Document{DocumentTypeId = documentTypeId, Number = number},
                new Document{DocumentTypeId = documentTypeId + 1, Number = $"a{number}a"},
                new Document{DocumentTypeId = documentTypeId, Number = $"b{number}"},
                new Document{DocumentTypeId = documentTypeId + 2, Number = number}
            };
            var dbSet = documents.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<Document>()).Returns(dbSet.Object);
            IDocumentsRepository repository = new DocumentsRepository(context.Object);

            //Act
            var result = repository.Exists(documentTypeId, number).Result;
            var failed = repository.Exists(documentTypeId + 1, number).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
            Assert.IsFalse(failed);
        }
    }
}
