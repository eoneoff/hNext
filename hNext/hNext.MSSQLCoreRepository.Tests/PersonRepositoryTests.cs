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
    public class PersonRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void ExistReturnsCorrect()
        {
            //Arrange
            long id = 1;
            string firstName = "test first name";
            string familyName = "test family name";
            string patronimic = "test patronimic";
            DateTime date = DateTime.Today;
            var people = new List<Person>
            {
                new Person {Id = id, FirstName = firstName, FamilyName = familyName, Patronimic = patronimic, DateOfBirth = date},
                new Person{Id = id+1, FamilyName = $"aaa{firstName}", FirstName=familyName, Patronimic=$"{patronimic}bbb", DateOfBirth = date.AddDays(1) },
                new Person{Id = id+3, FamilyName = familyName, FirstName = firstName, Patronimic = patronimic, DateOfBirth = date}
            };
            var dbSet = people.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<Person>()).Returns(dbSet.Object);
            IPersonRepository repository = new PersonRepository(context.Object);

            //Act
            var result = repository.Exists(new Person
            {
                Id = id+4,
                FirstName = firstName,
                FamilyName = familyName,
                Patronimic = patronimic,
                DateOfBirth = date
            }).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Person>));
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(p => p.FirstName == firstName));
            Assert.IsTrue(result.All(p => p.FamilyName == familyName));
            Assert.IsTrue(result.All(p => p.Patronimic == patronimic));
            Assert.IsTrue(result.All(p => p.DateOfBirth?.Date == date.Date));
            Assert.IsFalse(result.Any(p => p.Id == id + 4));
        }
    }
}
