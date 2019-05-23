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
    public class EmailRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void BelongsToOthersReturnsCorrectly()
        {
            //Arrange
            long emailId = 1;
            List<PersonEmails> emails = new List<PersonEmails>
            {
                new PersonEmails{EmailId = emailId},
                new PersonEmails{EmailId = emailId},
                new PersonEmails{EmailId = emailId + 1},
                new PersonEmails {EmailId = emailId +2 }
            };
            var dbSet = emails.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.PersonEmails).Returns(dbSet.Object);
            IEmailRepository repository = new EmailRepository(context.Object);

            //Act
            var result = repository.BelongToOthers(emailId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExistsReturnsCorrectly()
        {
            //Arrange
            string address = "test";
            List<Email> emails = new List<Email>
            {
                new Email{Address = address},
                new Email {Address = $"aa{address}" },
                new Email {Address = $"{address}bb"}
            };
            var dbSet = emails.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<Email>()).Returns(dbSet.Object);
            IEmailRepository repository = new EmailRepository(context.Object);

            //Act
            var result = repository.Exists(address).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Email));
            Assert.AreEqual(address, result.Address);
        }
    }
}
