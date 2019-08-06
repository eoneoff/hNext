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
    public class CaseHistoryRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
        private CaseHistoryRepository repository;

        [TestMethod]
        public void InfoReturntesCaseHistory()
        {
            //Arrange
            long caseHistoryId = 1;
            List<CaseHistory> histories = new List<CaseHistory>
            {
                new CaseHistory{Id = caseHistoryId},
                new CaseHistory{Id = caseHistoryId + 1},
                new CaseHistory{Id = caseHistoryId + 2},
                new CaseHistory{Id = caseHistoryId + 3}
            };
            var dbSet = histories.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<CaseHistory>()).Returns(dbSet.Object);
            repository = new CaseHistoryRepository(context.Object);

            //Act
            var result = repository.Info(caseHistoryId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CaseHistory));
            Assert.AreEqual(caseHistoryId, result.Id);
        }
    }
}
