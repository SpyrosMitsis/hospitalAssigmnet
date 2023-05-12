using Castle.Core.Logging;
using FakeItEasy;
using FluentAssertions;
using hospitals.Controllers;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Tests.ControllerTests
{
    public class DoctorControllerTests
    {
        private DoctorController _doctorController;
        private IDoctorRepository _doctorRepository;
        private IEmployeeRepository _employeeRepository;
        private ILogger<DoctorController> _logger;

        public DoctorControllerTests()
        {
            // Dependencies
            _doctorRepository = A.Fake<IDoctorRepository>();
            _employeeRepository = A.Fake<IEmployeeRepository>();
            _logger = A.Fake<ILogger<DoctorController>>();
            // SUT
            _doctorController = new DoctorController(_employeeRepository, _doctorRepository, _logger);
        }
        [Fact]
        public void DoctorController_Index_ReturnsSuccess()
        {
            // Arrange
            var doctors = A.Fake<IEnumerable<Doctor>>();
            A.CallTo(() => _employeeRepository.GetAll<Doctor>()).Returns(doctors);
            // Act
            var result = _doctorController.Index();
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Fact]
        public void DoctorController_Detail_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var doctor = A.Fake<Doctor>();
            A.CallTo(() => _doctorRepository.GetByIdAsync(id)).Returns(doctor);
            // Act
            var result = _doctorController.Detail(id);
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
