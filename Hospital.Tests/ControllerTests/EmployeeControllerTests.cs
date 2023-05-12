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
    public class EmployeeControllerTests
    {
        private IEmployeeRepository _employeeRepository;
        private EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            // Dependencies
            _employeeRepository = A.Fake<IEmployeeRepository>();
            // SUT
            _employeeController = new EmployeeController(_employeeRepository);
        }
        [Fact]
        public void EmployeeController_Index_ReturnsSuccess()
        {
            // Arrange
            var employees = A.Fake<IEnumerable<Employee>>();
            A.CallTo(() => _employeeRepository.GetAll<Employee>()).Returns(employees);
            // Act
            var result = _employeeController.Index();
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
    }
}
