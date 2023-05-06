using FakeItEasy;
using FluentAssertions;
using hospitals.Controllers;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Tests.ControllerTests
{
    public class AddressControllerTests
    {
        private AddressController _addressController;
        private IAddressRepository _addressRepository;
        public AddressControllerTests()
        {
            // Dependencies
            _addressRepository = A.Fake<IAddressRepository>();
            // SUT
            _addressController = new AddressController(_addressRepository);
        }
        [Fact]
        public void AddressController_Index_ReturnsSuccess()
        {
            // Arrange
            var addresses = A.Fake<IEnumerable<Address>>();
            A.CallTo(() => _addressRepository.GetAll()).Returns(addresses);
            // Act
            var result = _addressController.Index();
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Fact]
        public void AddressController_Detail_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var address = A.Fake<Address>();
            A.CallTo(() => _addressRepository.GetByIdAsync(id)).Returns(address);
            // Act
            var result = _addressController.Detail(id);
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
