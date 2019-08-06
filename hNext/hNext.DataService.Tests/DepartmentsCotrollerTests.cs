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
    public class DepartmentsCotrollerTests
    {
        private Mock<IDepartmentRepository> repository = new Mock<IDepartmentRepository>();
        private Mock<IRepository<DepartmentPhone>> phoneRepository = new Mock<IRepository<DepartmentPhone>>();
        private Mock<IRepository<DepartmentEmail>> emailRepository = new Mock<IRepository<DepartmentEmail>>();
        private Mock<IRepository<DepartmentSpecialty>> specialtyRepository = new Mock<IRepository<DepartmentSpecialty>>();
        private Mock<IGetter<Specialty>> specialtyGetter = new Mock<IGetter<Specialty>>();
        private DepartmentsController controller;

        public DepartmentsCotrollerTests()
        {
            controller = new DepartmentsController(repository.Object, specialtyRepository.Object,
                phoneRepository.Object, emailRepository.Object, specialtyGetter.Object);
        }

        [TestMethod]
        public void GetReturnsListOfDepartments()
        {
            //Arrange
            repository.Setup(r => r.Get()).ReturnsAsync(new List<Department>() as IEnumerable<Department>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Department>));
        }

        [TestMethod]
        public void GetIdReturnsDepartment()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] key) =>
            {
                return new Department { Id = (int)key[0] };
            });
            int departmentId = 1;

            //Act
            var result = controller.Get(departmentId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Department));
            Assert.AreEqual(departmentId, result.Id);
        }

        [TestMethod]
        public void ExistsReturnsDepartment()
        {
            //Arrange
            repository.Setup(r => r.Exists(It.IsAny<Department>())).ReturnsAsync((Department d) => { return d; });
            int hospitalId = 1;
            string departmentName = "test name";

            //Act
            var result = (controller.Exists(new Department { HospitalId = hospitalId, Name = departmentName }).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Department));
            Assert.AreEqual(hospitalId, (result as Department)?.HospitalId);
            Assert.AreEqual(departmentName, (result as Department)?.Name);
        }

        [TestMethod]
        public void PostReturnsDepartmentl()
        {
            //Arrange
            repository.Setup(r => r.Post(It.IsAny<Department>())).ReturnsAsync((Department d) =>
            {
                return d;
            });
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            int departmentId = 3;

            //Act
            var result = (controller.Post(new Department { Id = departmentId }).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Department));
            Assert.AreEqual(departmentId, (result as Department)?.Id);
        }

        [TestMethod]
        public void PutReturnsDepartment()
        {
            //Arrange
            repository.Setup(r => r.Put(It.IsAny<Department>())).ReturnsAsync((Department d) =>
            {
                return d;
            });
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            int departmentId = 3;

            //Act
            var result = (controller.Put(departmentId, new Department { Id = departmentId }).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Department));
            Assert.AreEqual(departmentId, (result as Department)?.Id);
        }

        [TestMethod]
        public void AddEmailReturnsEmail()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Department
                {
                    Id = (int)id[0],
                    Emails = new List<DepartmentEmail>()
                };
            });
            repository.Setup(r => r.Put(It.IsAny<Department>())).ReturnsAsync((Department d) =>
            {
                return d;
            });
            int departmentId = 1;
            long emailId = departmentId + 1;

            //Act
            var result = (controller.AddEmail(departmentId, new DepartmentEmail { DepartmentId = departmentId, EmailId = emailId })
                .Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DepartmentEmail));
            Assert.AreEqual(departmentId, (result as DepartmentEmail)?.DepartmentId);
            Assert.AreEqual(emailId, (result as DepartmentEmail)?.EmailId);
        }

        [TestMethod]
        public void DeleteEmailReturnsDeparmentEmail()
        {
            //Arrange
            int departmentId = 1;
            long emailId = departmentId + 1;
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Department
                {
                    Id = departmentId,
                    Emails = new List<DepartmentEmail>
                    {
                        new DepartmentEmail
                        {
                            DepartmentId = departmentId,
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
                return new DepartmentEmail
                {
                    DepartmentId = (int)id[0],
                    EmailId = (long)id[1]
                };
            });

            //Act
            var result = (controller.DeleteEmail(departmentId, emailId).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DepartmentEmail));
            Assert.AreEqual(departmentId, (result as DepartmentEmail)?.DepartmentId);
            Assert.AreEqual(emailId, (result as DepartmentEmail)?.EmailId);
        }

        [TestMethod]
        public void AddPhoneReturnsDepartmentPhone()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Department
                {
                    Id = (int)id[0],
                    Phones = new List<DepartmentPhone>()
                };
            });
            repository.Setup(r => r.Put(It.IsAny<Department>())).ReturnsAsync((Department d) =>
            {
                return d;
            });
            int departmentId = 1;
            long phoneId = departmentId + 1;

            //Act
            var result = (controller.AddPhone(departmentId, new DepartmentPhone { DepartmentId = departmentId, PhoneId = phoneId })
                .Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DepartmentPhone));
            Assert.AreEqual(departmentId, (result as DepartmentPhone)?.DepartmentId);
            Assert.AreEqual(phoneId, (result as DepartmentPhone)?.PhoneId);
        }

        [TestMethod]
        public void DeletePhoneReturnsDepartmentPhone()
        {
            //Arrange
            int departmentId = 1;
            long phoneId = departmentId + 1;
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Department
                {
                    Id = departmentId,
                    Phones = new List<DepartmentPhone>
                    {
                        new DepartmentPhone
                        {
                            DepartmentId = departmentId,
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
                return new DepartmentPhone
                {
                    DepartmentId = (int)id[0],
                    PhoneId = (long)id[1]
                };
            });

            //Act
            var result = (controller.DeletePhone(departmentId, phoneId).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DepartmentPhone));
            Assert.AreEqual(departmentId, (result as DepartmentPhone)?.DepartmentId);
            Assert.AreEqual(phoneId, (result as DepartmentPhone)?.PhoneId);
        }

        [TestMethod]
        public void AddSpecialtyReturnsDepartmentSpecialty()
        {
            //Arrange
            repository.Setup(s => s.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            specialtyGetter.Setup(g => g.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            specialtyRepository.Setup(s => s.Post(It.IsAny<DepartmentSpecialty>())).ReturnsAsync(
                (DepartmentSpecialty ds) => { return ds; });
            int departmentId = 1;
            int specialtyId = departmentId + 1;

            //Act
            var result = (controller.AddSpecialty(departmentId, specialtyId).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DepartmentSpecialty));
            Assert.AreEqual((result as DepartmentSpecialty).DeparmentId, departmentId);
            Assert.AreEqual((result as DepartmentSpecialty).SpecialtyId, specialtyId);
        }

        [TestMethod]
        public void DeleteSpecialtyReturnsDepartmentSpecialty()
        {
            //Arrange
            specialtyRepository.Setup(s => s.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            specialtyRepository.Setup(s => s.Delete(It.IsAny<object[]>())).ReturnsAsync(
                (object[] keys) =>
                {
                    return new DepartmentSpecialty
                    {
                        DeparmentId = (int)keys[0],
                        SpecialtyId = (int)keys[1]
                    };
                });
            int departmentId = 1;
            int specialtyId = departmentId + 1;

            //Act
            var result = (controller.DeleteSpecialty(departmentId, specialtyId).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DepartmentSpecialty));
            Assert.AreEqual((result as DepartmentSpecialty).DeparmentId, departmentId);
            Assert.AreEqual((result as DepartmentSpecialty).SpecialtyId, specialtyId);
        }
    }
}
