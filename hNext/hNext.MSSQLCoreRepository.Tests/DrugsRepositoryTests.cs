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
    public class DrugsRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void SearchReturnsCorrectly()
        {
            //Arrange
            string name = "test1";
            var drugs = new List<Drug>
            {
                new Drug{Name = name, InternationalName="aaaaa"},
                new Drug{Name="bbb", InternationalName=$"bbb{name}"},
                new Drug{Name = "aaaaaa", InternationalName="bbbb"}
            }.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<Drug>()).Returns(drugs.Object);
            var repository = new DrugRepository(context.Object);

            //Act
            var result = repository.Search(name).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Drug>));
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(d => d.Name.Contains(name) || d.InternationalName.Contains(name)));
        }
    }
}