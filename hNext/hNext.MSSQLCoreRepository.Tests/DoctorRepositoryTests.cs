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
    public class DoctorRepositoryTests
    {
        private Mock<hNextDbContext> context = new Mock<hNextDbContext>(new DbContextOptions<hNextDbContext>());
        [TestMethod]
        public void SearchDoctorReturnstListOfDoctors()
        {
            //Arrange
            var doctors = new List<Doctor>().AsQueryable().BuildMockDbSet();
            context.Setup(c => c.SearchDoctor(It.IsAny<DoctorSearchModel>())).Returns(doctors.Object);
            var repository = new DoctorRepository(context.Object);

            //Act
            var result = repository.SearchDoctors(new DoctorSearchModel()).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Doctor>));
        }
    }
}
