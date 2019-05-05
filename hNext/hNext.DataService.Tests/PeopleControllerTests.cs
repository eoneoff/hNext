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
    public class PeopleControllerTests
    {
        private Mock<IPersonRepository> moq = new Mock<IPersonRepository>();
        private Mock<IRepository<PersonPhone>> moqPhone = new Mock<IRepository<PersonPhone>>();
        private Mock<IRepository<PersonEmails>> moqEmail = new Mock<IRepository<PersonEmails>>();

        private PeopleController controller;

        public PeopleControllerTests()
        {
            controller = new PeopleController(moq.Object, moqPhone.Object, moqEmail.Object);
        }

        [TestMethod]
        public void GetReturnsListOfPeople()
        {
            //arrange
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Person>() as IEnumerable<Person>));

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Person>));
        }

        [TestMethod]
        public void GetIdReturnsPerson()
        {
            //Arrange
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Person { Id = (long)id[0] }));
            long personId = 3;

            //Act
            var result = controller.Get(personId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Person));
            Assert.AreEqual(personId, result.Id);
        }

        [TestMethod]
        public void SearchReturnsListOfPeople()
        {
            //Assert
            moq.Setup(m => m.Search(It.IsAny<string[]>())).Returns(Task.FromResult(new List<Person>() as IEnumerable<Person>));

            //Act
            var result = controller.Search().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Person>));
        }

        [TestMethod]
        public void ExstsReturnsListOfPersons()
        {
            //Assert
            moq.Setup(m => m.Exists(It.IsAny<Person>())).Returns<Person>(p => Task.FromResult(new List<Person> { p} as IEnumerable<Person>));
            long personId = 3;

            //Act
            var result = controller.Exists(new Person { Id = personId }).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Person>));
            Assert.AreEqual(personId, (result as List<Person>)?[0]?.Id);
        }

        [TestMethod]
        public void PostReturnsPerson()
        {
            //Arrange
            moq.Setup(m => m.Post(It.IsAny<Person>())).Returns<Person>(p => Task.FromResult(p));
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));
            long personId = 3;

            //Act
            var result = (controller.Post(new Person { Id = personId }).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Person));
            Assert.AreEqual(personId, (result as Person)?.Id);
        }

        [TestMethod]
        public void PutReturnsPerson()
        {
            //Arrange
            moq.Setup(m => m.Put(It.IsAny<Person>())).Returns<Person>(p => Task.FromResult(p));
            moq.Setup(m => m.Exists(It.IsAny<object[]>())).Returns(Task.FromResult(true));
            long personId = 3;

            //Act
            var result = (controller.Put(personId, new Person { Id = personId }).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Person));
            Assert.AreEqual(personId, (result as Person)?.Id);
        }

        [TestMethod]
        public void AddPhoneReturnsPersonPhone()
        {
            //Arrange
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Person
            {
                Id = (long)id[0],
                Phones = new List<PersonPhone>()
            }));
            moq.Setup(m => m.Put(It.IsAny<Person>())).Returns<Person>(p => Task.FromResult(p));
            long personId = 1;
            long phoneId = personId + 1;

            //Act
            var result = (controller.AddPhone(new PersonPhone { PersonId = personId, PhoneId = phoneId }).Result
                as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(PersonPhone));
            Assert.AreEqual(personId, (result as PersonPhone)?.PersonId);
            Assert.AreEqual(phoneId, (result as PersonPhone)?.PhoneId);
        }

        [TestMethod]
        public void RemovePhoneReturnsRemovePhone()
        {
            //Arrange
            long personId = 1;
            long phoneId = personId + 1;
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Person
            {
                Id = (long)id[0],
                Phones = new List<PersonPhone>
                {
                    new PersonPhone
                    {
                        PersonId = personId,
                        PhoneId = phoneId,
                        Phone = new Phone
                        {
                            Id = phoneId
                        }
                    }
                }
            }));
            moqPhone.Setup(m => m.Delete(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new PersonPhone
            {
                PersonId = (long)id[0],
                PhoneId = (long)id[1]
            }));

            //Act
            var result = (controller.RemovePhone(personId, phoneId).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(PersonPhone));
            Assert.AreEqual(personId, (result as PersonPhone)?.PersonId);
            Assert.AreEqual(phoneId, (result as PersonPhone)?.PhoneId);
        }

        [TestMethod]
        public void AddEmailReturnsPersonEmail()
        {
            //Arrange
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Person
            {
                Id = (long)id[0],
                Emails = new List<PersonEmails>()
            }));
            moq.Setup(m => m.Put(It.IsAny<Person>())).Returns<Person>(p => Task.FromResult(p));
            long personId = 1;
            long emailId = personId + 1;

            //Act
            var result = (controller.AddEmail(new PersonEmails { PersonId = personId, EmailId = emailId }).Result
                as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(PersonEmails));
            Assert.AreEqual(personId, (result as PersonEmails)?.PersonId);
            Assert.AreEqual(emailId, (result as PersonEmails)?.EmailId);
        }

        [TestMethod]
        public void RemoveEmailReturnsPersonEmail()
        {
            //Arrange
            long personId = 1;
            long emailId = personId + 1;
            moq.Setup(m => m.Get(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new Person
            {
                Id = (long)id[0],
                Emails = new List<PersonEmails>
                {
                    new PersonEmails
                    {
                        PersonId = personId,
                        EmailId = emailId,
                        Email = new Email
                        {
                            Id = emailId
                        }
                    }
                }
            }));
            moqEmail.Setup(m => m.Delete(It.IsAny<object[]>())).Returns<object[]>(id => Task.FromResult(new PersonEmails
            {
                PersonId = (long)id[0],
                EmailId = (long)id[1]
            }));

            //Act
            var result = (controller.RemoveEmail(personId, emailId).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(PersonEmails));
            Assert.AreEqual(personId, (result as PersonEmails)?.PersonId);
            Assert.AreEqual(emailId, (result as PersonEmails)?.EmailId);
        }
    }
}
