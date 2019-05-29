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
    public class DepartmentRepositoryTests
    {
        Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());

        [TestMethod]
        public void ExistsReturnsCorrectly()
        {
            //Arrange
            string name = "department name";
            int hospitalId = 1;
            var departments = new List<Department>
            {
                new Department{HospitalId = hospitalId, Name = name},
                new Department{HospitalId = hospitalId +1, Name = name},
                new Department{HospitalId = hospitalId, Name=$"{name}aaa"}
            };
            var dbSet = departments.AsQueryable().BuildMockDbSet();
            context.Setup(c => c.Set<Department>()).Returns(dbSet.Object);
            IDepartmentRepository repository = new DepartmentRepository(context.Object);

            //Act
            var result = repository.Exists(new Department { HospitalId = hospitalId, Name = name }).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Department));
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(hospitalId, result.HospitalId);
        }
    }
}
