using hNext.DbAccessMSSQLCore;
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
    public class ICDRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
        [TestMethod]
        public void SearchReturnsICD()
        {
            //Arrange
            string letter = "A";
            int primary = 1;
            int secondary = 1;

            var icds = new List<ICD>
            {
                new ICD{Letter = letter, PrimaryNumber = primary, SecondaryNumber = secondary},
                new ICD{Letter = letter, PrimaryNumber = primary+1, SecondaryNumber = secondary+1},
                new ICD{Letter = "Z", PrimaryNumber = primary, SecondaryNumber = secondary}
            };
            var dbSet = icds.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<ICD>()).Returns(dbSet.Object);
            var repository = new ICDRepository(context.Object);

            //Act
            var result = repository.Search(new ICD
            {
                Letter = letter,
                PrimaryNumber = primary,
                SecondaryNumber = secondary
            }).Result;

            Assert.IsInstanceOfType(result, typeof(ICD));
            Assert.AreEqual(letter, result.Letter);
            Assert.AreEqual(primary, result.PrimaryNumber);
            Assert.AreEqual(secondary, result.SecondaryNumber);
        }
    }
}
