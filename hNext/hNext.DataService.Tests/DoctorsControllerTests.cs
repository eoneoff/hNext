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
    public class DoctorsControllerTests
    {
        private Mock<IDoctorRepository> repository = new Mock<IDoctorRepository>();
        private Mock<IDoctorSpecialtyRepository> specialtyRepository = new Mock<IDoctorSpecialtyRepository>();
        private Mock<IGetter<Specialty>> specialtyGetter = new Mock<IGetter<Specialty>>();
        private Mock<IDoctorPositionRepository> positionRepository = new Mock<IDoctorPositionRepository>();
        private Mock<IRepository<Diploma>> diplomaRepository = new Mock<IRepository<Diploma>>();
        private DoctorsController controller;

        public DoctorsControllerTests()
        {
            controller = new DoctorsController(repository.Object,
                                                specialtyRepository.Object,
                                                specialtyGetter.Object,
                                                positionRepository.Object,
                                                diplomaRepository.Object);
        }

        [TestMethod]
        public void GetReturnsListOfDoctors()
        {
            //Arrange
            repository.Setup(r => r.Get()).ReturnsAsync(new List<Doctor>() as IEnumerable<Doctor>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Doctor>));
        }

        [TestMethod]
        public void GetIdReturnsDoctor()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] key) =>
            {
                return new Doctor { Id = (int)key[0] };
            });
            int doctorId = 1;

            //Act
            var result = controller.Get(doctorId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Doctor));
            Assert.AreEqual(doctorId, result.Id);
        }

        [TestMethod]
        public void PostReturnsDoctor()
        {
            //Arrange
            repository.Setup(r => r.Post(It.IsAny<Doctor>())).ReturnsAsync((Doctor doctor) =>
            {
                return doctor;
            });
            int doctorId = 1;

            //Act
            var result = (controller.Post(new Doctor { Id = doctorId }).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Doctor));
            Assert.AreEqual(doctorId, (result as Doctor)?.Id);
        }

        [TestMethod]
        public void SearchReturnsListOfPatients()
        {
            //Arrange
            repository.Setup(r => r.SearchDoctors(It.IsAny<DoctorSearchModel>())).ReturnsAsync(new List<Doctor>()
                as IEnumerable<Doctor>);

            //Act
            var result = controller.Search(new DoctorSearchModel()).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Doctor>));
        }

        [TestMethod]
        public void PutReturnsDoctor()
        {
            //Arrange
            repository.Setup(r => r.Put(It.IsAny<Doctor>())).ReturnsAsync((Doctor doctor) =>
            {
                return doctor;
            });
            long doctorId = 1;

            //Act
            var result = (controller.Put(doctorId, new Doctor { Id = doctorId }).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Doctor));
            Assert.AreEqual(doctorId, (result as Doctor)?.Id);
        }

        [TestMethod]
        public void AddSpecialtyReturnsDoctorSpecialty()
        {
            //Arrange
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            specialtyGetter.Setup(g => g.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            specialtyRepository.Setup(s => s.Post(It.IsAny<DoctorSpecialty>())).ReturnsAsync(
                (DoctorSpecialty specialty)=> { return specialty; });
            int specialtyId = 1;
            long doctorId = specialtyId + 1;

            //Act
            var result = (controller.AddSpecialty(doctorId, new DoctorSpecialty
            {
                DoctorId = doctorId,
                SpecialtyId = specialtyId
            }).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorSpecialty));
            Assert.AreEqual(doctorId, (result as DoctorSpecialty)?.DoctorId);
            Assert.AreEqual(specialtyId, (result as DoctorSpecialty)?.SpecialtyId);
        }

        [TestMethod]
        public void EditSpecialtyReturnsDoctorSpecialty()
        {
            //Arrange
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            specialtyGetter.Setup(g => g.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            specialtyRepository.Setup(s => s.Put(It.IsAny<DoctorSpecialty>())).ReturnsAsync(
                (DoctorSpecialty specialty) => { return specialty; });
            int specialtyId = 1;
            long doctorId = specialtyId + 1;
            long doctorSpecialtyId = specialtyId + 2;

            //Act
            var result = (controller.EditSpecialty(doctorId, doctorSpecialtyId, new DoctorSpecialty
            {
                Id = doctorSpecialtyId,
                DoctorId = doctorId,
                SpecialtyId = specialtyId
            }).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorSpecialty));
            Assert.AreEqual(doctorSpecialtyId, (result as DoctorSpecialty)?.Id);
            Assert.AreEqual(doctorId, (result as DoctorSpecialty)?.DoctorId);
            Assert.AreEqual(specialtyId, (result as DoctorSpecialty)?.SpecialtyId);
        }

        [TestMethod]
        public void DeleteReturnsDoctorSpecialty()
        {
            //Arrange
            long id = 1;
            specialtyRepository.Setup(s => s.Exists(It.IsAny<long>(), It.IsAny<int>())).ReturnsAsync(
                new DoctorSpecialty { Id = id});
            specialtyRepository.Setup(s => s.Delete(It.IsAny<object[]>())).ReturnsAsync(
                (object[] key) => { return new DoctorSpecialty { Id = (long)key[0]}; });

            //Act
            var result = (controller.DeleteSpecialty(1, 2).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorSpecialty));
            Assert.AreEqual(id, (result as DoctorSpecialty)?.Id);
        }

        [TestMethod]
        public void SpecialtyExistsReturnsDoctorSpecialty()
        {
            //Arrange
            int specialtyId = 1;
            long doctorId = specialtyId + 1;
            specialtyRepository.Setup(r => r.Exists(It.IsAny<long>(), It.IsAny<int>())).ReturnsAsync(
                (long dId, int sId) =>
                {
                    return new DoctorSpecialty
                    {
                        DoctorId = dId,
                        SpecialtyId = sId
                    };
                });

            //Act
            var result = controller.SpecialtyExists(doctorId, specialtyId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorSpecialty));
            Assert.AreEqual(doctorId, result.DoctorId);
            Assert.AreEqual(specialtyId, result.SpecialtyId);
        }

        [TestMethod]
        public void AddPositionReturnsDoctorPosition()
        {
            //Arrange
            positionRepository.Setup(s => s.Exists(It.IsAny<DoctorPosition>())).ReturnsAsync(default(DoctorPosition));
            positionRepository.Setup(s => s.Post(It.IsAny<DoctorPosition>())).ReturnsAsync(
                (DoctorPosition dp) => { return dp; });
            long doctorId = 1;

            //Act
            var result = (controller.AddPosition(doctorId, new DoctorPosition { DoctorId = doctorId }).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorPosition));
            Assert.AreEqual(doctorId, (result as DoctorPosition)?.DoctorId);
        }

        [TestMethod]
        public void EditPositionRetunrsDoctorPosition()
        {
            //Arrange
            positionRepository.Setup(p => p.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            positionRepository.Setup(p => p.Put(It.IsAny<DoctorPosition>())).ReturnsAsync(
                (DoctorPosition dp) => { return dp; });
            long doctorId = 1;
            long positionId = doctorId + 1;

            //Act
            var result = (controller.EditPosition(doctorId, positionId, new DoctorPosition
            {
                DoctorId = doctorId,
                Id = positionId
            }).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorPosition));
            Assert.AreEqual(doctorId, (result as DoctorPosition)?.DoctorId);
            Assert.AreEqual(positionId, (result as DoctorPosition)?.Id);
        }

        [TestMethod]
        public void DeletePositionReturnsDoctorPosition()
        {
            //Arrange
            long doctorId = 1;
            long positionId = doctorId + 1;
            positionRepository.Setup(p => p.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            positionRepository.Setup(p => p.Delete(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
                { return new DoctorPosition { Id = (long)id[0] }; });
            positionRepository.Setup(p => p.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
                { return new DoctorPosition { Id = (long)id[0], DoctorId = doctorId }; });

            //Act
            var result = (controller.DeletePosition(doctorId, positionId).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorPosition));
            Assert.AreEqual(positionId, (result as DoctorPosition)?.Id);
        }

        [TestMethod]
        public void AddDiplomaReturnsDiploma()
        {
            //Arrange
            diplomaRepository.Setup(d => d.Post(It.IsAny<Diploma>())).ReturnsAsync((Diploma d) => { return d; });
            long doctorId = 1;

            //Act
            var result = (controller.AddDiplomaToDoctor(doctorId, new Diploma()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Diploma));
            Assert.AreEqual(doctorId, (result as Diploma)?.DoctorId);
        }

        [TestMethod]
        public void EditDiplomaReturnsDiploma()
        {
            //Arrange
            diplomaRepository.Setup(d => d.Put(It.IsAny<Diploma>())).ReturnsAsync((Diploma d) => { return d; });
            diplomaRepository.Setup(d => d.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            long doctorId = 1;
            long diplomaId = doctorId + 1;

            //Act
            var result = (controller.EditDiploma(doctorId, diplomaId, new Diploma
            {
                Id = diplomaId,
                DoctorId = doctorId
            }).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Diploma));
            Assert.AreEqual(doctorId, (result as Diploma)?.DoctorId);
            Assert.AreEqual(diplomaId, (result as Diploma)?.Id);
        }

        [TestMethod]
        public void DeleteDiplomaReturnsDiploma()
        {
            //Arrange
            long doctorId = 1;
            long diplomaId = doctorId + 1;
            diplomaRepository.Setup(d => d.Get(It.IsAny<object[]>())).ReturnsAsync((object[] key) =>
            {
                return new Diploma
                {
                    Id = (long)key[0],
                    DoctorId = doctorId
                };
            });
            diplomaRepository.Setup(d => d.Delete(It.IsAny<object[]>())).ReturnsAsync((object[] key) =>
            {
                return new Diploma
                {
                    Id = (long)key[0]
                };
            });

            //Act
            var result = (controller.DeleteDiploma(doctorId, diplomaId).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Diploma));
            Assert.AreEqual(diplomaId, (result as Diploma)?.Id);
        }
    }
}
