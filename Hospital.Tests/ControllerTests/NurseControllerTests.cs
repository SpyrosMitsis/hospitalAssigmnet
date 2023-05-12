using FakeItEasy;
using FluentAssertions;
using hospitals.Controllers;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Tests.ControllerTests
{
    public class NurseControllerTests
    {
        private INurseRepository _nurseRepository;
        private IEmployeeRepository _employeeRepository;
        private NurseController _nurseController;

        public NurseControllerTests()
        {
            // Dependencies
            _nurseRepository = A.Fake<INurseRepository>();
            _employeeRepository = A.Fake<IEmployeeRepository>();
            // SUT
            _nurseController = new NurseController(_nurseRepository, _employeeRepository);
        }
        [Fact]
        public void NurseController_Index_ReturnsSuccess()
        {
            // Arrange
            var nurses = A.Fake<IEnumerable<Nurse>>();
            A.CallTo(() => _employeeRepository.GetAll<Nurse>()).Returns(nurses);
            // Act
            var result = _nurseController.Index();
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Fact]
        public void NurseController_Detail_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var nurse = A.Fake<Nurse>();
            A.CallTo(() => _nurseRepository.GetByIdAsync(id)).Returns(nurse);
            // Act
            var result = _nurseController.Detail(id);
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
