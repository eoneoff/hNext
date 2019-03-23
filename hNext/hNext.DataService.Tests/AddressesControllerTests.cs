using hNext.DataService.Controllers;
using hNext.IRepository;
using hNext.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hNext.DataService.Tests
{
    [TestClass]
    public class AddressesControllerTests
    {
        [TestMethod]
        public void GetReturnsListOfAddresses()
        {
            //Arrange
            var moq = new Mock<IAddressRepository>();
            moq.Setup(m => m.Get()).Returns(Task.FromResult(new List<Address>() as IEnumerable<Address>));
            AddressesController controller = new AddressesController(moq.Object);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Address>));
        }

        [TestMethod]
        public void GetIdReturnsAddress()
        {
            //Arrange
            var moq = new Mock<IAddressRepository>();
            moq.Setup(m => m.Get(It.IsAny<long>())).Returns<long>(id => Task.FromResult(new Address { Id = id }));
            AddressesController controller = new AddressesController(moq.Object);
            long addressId = 3;

            //Act
            var result = controller.Get(addressId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Address));
            Assert.AreEqual(addressId, result.Id);
        }

        [TestMethod]
        public void ExistsChecksCorrectly()
        {
            var moq = new Mock<IAddressRepository>();
            Address address = new Address { Id = 5 };
            moq.Setup(m => m.Exists(It.IsAny<Address>())).Returns(Task.FromResult(address));
            AddressesController controller = new AddressesController(moq.Object);

            //Act
            var result = controller.Exists(new Address()).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Address));
            Assert.AreEqual(address.Id, result.Id);
        }
    }
}
