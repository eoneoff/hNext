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
    public class PhoneRepositlryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void ExistsReturnsCorrect()
        {
            //Arrange
            string number = "test number";
            var phones = new List<Phone>
            {
                new Phone{Number = number},
                new Phone{Number = $"aaa{number}"},
                new Phone{Number = $"{number}bbb" }
            };
            var dbSet = phones.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<Phone>()).Returns(dbSet.Object);
            IPhoneRepository repository = new PhoneRepository(context.Object);

            //Act
            var result = repository.Exists(number).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Phone));
            Assert.AreEqual(number, result.Number);
        }

        [TestMethod]
        public void BelongToOthersReturnsCorrect()
        {
            //Arrange
            long phoneId = 1;
            var phones = new List<PersonPhone>
            {
                new PersonPhone{PhoneId = phoneId},
                new PersonPhone{PhoneId = phoneId +1},
                new PersonPhone{PhoneId = phoneId + 2}
            };
            var dbSet = phones.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.PersonPhones).Returns(dbSet.Object);
            IPhoneRepository repository = new PhoneRepository(context.Object);

            //Act
            var belongs = repository.BelongToOthers(phoneId).Result;
            var notBelongs = repository.BelongToOthers(phoneId + 10).Result;

            //Assert
            Assert.IsInstanceOfType(belongs, typeof(bool));
            Assert.IsInstanceOfType(notBelongs, typeof(bool));
            Assert.IsTrue(belongs);
            Assert.IsFalse(notBelongs);
        }
    }
}
