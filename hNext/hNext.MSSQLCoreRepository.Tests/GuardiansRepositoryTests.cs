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
    public class GuardiansRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void ExistsReturnsCorrectly()
        {
            //Arrange
            long wardId = 1;
            long guardianId = 10;
            string relation = "test";
            var guardians = new List<GuardianWard>
            {
                new GuardianWard
                {
                    WardId = wardId,
                    GuardianId = guardianId,
                    Relation = relation
                },
                new GuardianWard
                {
                    WardId = wardId,
                    GuardianId = guardianId + 1,
                    Relation = relation
                },
                new GuardianWard
                {
                    WardId = wardId + 1,
                    GuardianId = guardianId,
                    Relation = $"aa{relation}"
                }
            };
            var dbSet = guardians.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<GuardianWard>()).Returns(dbSet.Object);
            IGuardianRepository repository = new GuardianRepository(context.Object);

            //Act
            var result = repository.Exists(new GuardianWard
            {
                WardId = wardId,
                GuardianId = guardianId,
                Relation = relation
            }).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(GuardianWard));
            Assert.AreEqual(wardId, result.WardId);
            Assert.AreEqual(guardianId, result.GuardianId);
            Assert.AreEqual(relation, result.Relation);
        }
    }
}
