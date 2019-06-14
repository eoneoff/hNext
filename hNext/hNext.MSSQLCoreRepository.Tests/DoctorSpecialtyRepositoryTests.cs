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
    public class DoctorSpecialtyRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void ExistsReturnsDoctorSpecialty()
        {
            //Arrange
            int specialtyId = 1;
            long doctorId = specialtyId + 1;
            var specialties = new List<DoctorSpecialty>
            {
                new DoctorSpecialty{DoctorId = doctorId, SpecialtyId = specialtyId},
                new DoctorSpecialty {DoctorId = doctorId, SpecialtyId = specialtyId +1},
                new DoctorSpecialty{DoctorId = doctorId +1, SpecialtyId = specialtyId},
                new DoctorSpecialty{DoctorId = doctorId +1, SpecialtyId = specialtyId +1}
            };

            var dbSet = specialties.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<DoctorSpecialty>()).Returns(dbSet.Object);
            IDoctorSpecialtyRepository repository = new DoctorSpecialtyRepository(context.Object);

            //Act
            var result = repository.Exists(doctorId, specialtyId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorSpecialty));
            Assert.AreEqual(doctorId, result.DoctorId);
            Assert.AreEqual(specialtyId, result.SpecialtyId);
        }
    }
}
