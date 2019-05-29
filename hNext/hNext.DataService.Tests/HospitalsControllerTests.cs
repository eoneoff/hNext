using hNext.DataService.Controllers;
using hNext.IRepository;
using hNext.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace hNext.DataService.Tests
{
    [TestClass]
    public class HospitalsControllerTests
    {
        private Mock<IHospitalRepository> repository = new Mock<IHospitalRepository>();
        private Mock<IRepository<HospitalEmail>> emailRepository = new Mock<IRepository<HospitalEmail>>();
        private Mock<IRepository<HospitalPhone>> phoneRepository = new Mock<IRepository<HospitalPhone>>();
        private HospitalsController controller;

        public HospitalsControllerTests()
        {
            controller = new HospitalsController(repository.Object, emailRepository.Object,
                phoneRepository.Object);
        }

        [TestMethod]
        public void GetReturnsListOfHospitals()
        {
            //Arrange
            repository.Setup(r => r.Get()).ReturnsAsync(new List<Hospital>() as IEnumerable<Hospital>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Hospital>));
        }

        [TestMethod]
        public void GetIdReturnsHospital()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] key) =>
            {
                return new Hospital { Id = (int)key[0] };
            });
            int hospitalId = 3;

            //Act
            var result = controller.Get(hospitalId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Hospital));
            Assert.AreEqual(hospitalId, result.Id);
        }

        [TestMethod]
        public void PostReturnsHospital()
        {
            //Arrange
            repository.Setup(r => r.Post(It.IsAny<Hospital>())).ReturnsAsync((Hospital hospital) =>
            {
                return hospital;
            });
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            int hospitalId = 3;

            //Act
            var result = (controller.Post(new Hospital { Id = hospitalId }).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Hospital));
            Assert.AreEqual(hospitalId, (result as Hospital)?.Id);
        }

        [TestMethod]
        public void PutReturnsHospital()
        {
            //Arrange
            repository.Setup(r => r.Put(It.IsAny<Hospital>())).ReturnsAsync((Hospital hospital) =>
            {
                return hospital;
            });
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            int hospitalId = 3;

            //Act
            var result = (controller.Put(hospitalId, new Hospital { Id = hospitalId }).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Hospital));
            Assert.AreEqual(hospitalId, (result as Hospital)?.Id);
        }

        [TestMethod]
        public void ExistsReturnsBool()
        {
            //Arrange
            repository.Setup(r => r.Exists(It.IsAny<string>())).ReturnsAsync((string name) =>
            {
                return true;
            });

            //Act
            var result = controller.Exists("name").Result;

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddRemailReturnsEmail()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Hospital
                {
                    Id = (int)id[0],
                    Emails = new List<HospitalEmail>()
                };
            });
            repository.Setup(r => r.Put(It.IsAny<Hospital>())).ReturnsAsync((Hospital hospital) =>
            {
                return hospital;
            });
            int hospitalId = 1;
            long emailId = hospitalId + 1;

            //Act
            var result = (controller.AddEmail(hospitalId, new HospitalEmail { HospitalId = hospitalId, EmailId = emailId })
                .Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(HospitalEmail));
            Assert.AreEqual(hospitalId, (result as HospitalEmail)?.HospitalId);
            Assert.AreEqual(emailId, (result as HospitalEmail)?.EmailId);
        }

        [TestMethod]
        public void DeleteEmailReturnsHospitalEmail()
        {
            //Arrange
            int hospitalId = 1;
            long emailId = hospitalId + 1;
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Hospital
                {
                    Id = hospitalId,
                    Emails = new List<HospitalEmail>
                    {
                        new HospitalEmail
                        {
                            HospitalId = hospitalId,
                            EmailId = emailId,
                            Email = new Email
                            {
                                Id = emailId
                            }
                        }
                    }
                };
            });
            emailRepository.Setup(e => e.Delete(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new HospitalEmail
                {
                    HospitalId = (int)id[0],
                    EmailId = (long)id[1]
                };
            });

            //Act
            var result = (controller.DeleteEmail(hospitalId, emailId).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(HospitalEmail));
            Assert.AreEqual(hospitalId, (result as HospitalEmail)?.HospitalId);
            Assert.AreEqual(emailId, (result as HospitalEmail)?.EmailId);
        }

        [TestMethod]
        public void AddPhoneReturnsHospitalPhone()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Hospital
                {
                    Id = (int)id[0],
                    Phones = new List<HospitalPhone>()
                };
            });
            repository.Setup(r => r.Put(It.IsAny<Hospital>())).ReturnsAsync((Hospital hospital) =>
            {
                return hospital;
            });
            int hospitalId = 1;
            long phoneId = hospitalId + 1;

            //Act
            var result = (controller.AddPhone(hospitalId, new HospitalPhone { HospitalId = hospitalId, PhoneId = phoneId })
                .Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(HospitalPhone));
            Assert.AreEqual(hospitalId, (result as HospitalPhone)?.HospitalId);
            Assert.AreEqual(phoneId, (result as HospitalPhone)?.PhoneId);
        }

        [TestMethod]
        public void DeletePhoneReturnsHospitalPhone()
        {
            //Arrange
            int hospitalId = 1;
            long phoneId = hospitalId + 1;
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Hospital
                {
                    Id = hospitalId,
                    Phones = new List<HospitalPhone>
                    {
                        new HospitalPhone
                        {
                            HospitalId = hospitalId,
                            PhoneId = phoneId,
                            Phone = new Phone
                            {
                                Id = phoneId
                            }
                        }
                    }
                };
            });
            phoneRepository.Setup(e => e.Delete(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new HospitalPhone
                {
                    HospitalId = (int)id[0],
                    PhoneId = (long)id[1]
                };
            });

            //Act
            var result = (controller.DeletePhone(hospitalId, phoneId).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(HospitalPhone));
            Assert.AreEqual(hospitalId, (result as HospitalPhone)?.HospitalId);
            Assert.AreEqual(phoneId, (result as HospitalPhone)?.PhoneId);
        }
    }
}
