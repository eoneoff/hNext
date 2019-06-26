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
    public class DoctorPositionRepositoryTests
    {
        Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void ExistReturnsDoctorPosition()
        {
            //Arrange
            int positionId = 1;
            long doctorId = positionId + 1;
            int hospitalId = positionId + 2;
            int? departmentId = positionId + 3;
            int specialtyId = positionId + 4;
            List<DoctorPosition> positions = new List<DoctorPosition>
            {
                new DoctorPosition
                {
                    DoctorId = doctorId,
                    PositionId = positionId,
                    HospitalId = hospitalId,
                    DepartmentId = departmentId,
                    SpecialtyId = specialtyId
                },
                new DoctorPosition
                {
                    DoctorId = doctorId + 1,
                    PositionId = positionId,
                    HospitalId = hospitalId - 1,
                    DepartmentId = departmentId,
                    SpecialtyId = specialtyId
                },
                new DoctorPosition
                {
                    DoctorId = doctorId + 2,
                    PositionId = positionId + 1,
                    HospitalId = hospitalId,
                    DepartmentId = departmentId + 1,
                    SpecialtyId = specialtyId + 1
                }
            };
            var dbSet = positions.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<DoctorPosition>()).Returns(dbSet.Object);
            var repository = new DoctorPositionRepository(context.Object);

            //Act
            var result = repository.Exists(new DoctorPosition
            {
                DoctorId = doctorId,
                PositionId = positionId,
                SpecialtyId = specialtyId,
                HospitalId = hospitalId,
                DepartmentId = departmentId
            }).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(DoctorPosition));
            Assert.AreEqual(doctorId, result.DoctorId);
            Assert.AreEqual(specialtyId, result.SpecialtyId);
            Assert.AreEqual(departmentId, result.DepartmentId);
            Assert.AreEqual(hospitalId, result.HospitalId);
            Assert.AreEqual(departmentId, result.DepartmentId);
        }
    }
}
