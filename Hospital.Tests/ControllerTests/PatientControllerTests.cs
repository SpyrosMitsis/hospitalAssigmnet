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
    public class PatientControllerTests
    {
        private IPatientRepository _patientRepository;
        private PatientController _patientController;

        public PatientControllerTests()
        {
            // Dependencies
            _patientRepository = A.Fake<IPatientRepository>();
            // SUT
            _patientController = new PatientController(_patientRepository);
        }

        [Fact]
        public void PatientController_Index_ReturnsSuccess()
        {
            // Arrange
            var patients = A.Fake<IEnumerable<Patient>>();
            A.CallTo(() => _patientRepository.GetAll()).Returns(patients);
            // Act
            var result = _patientController.Index();
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Fact]
        public void PatientController_Detail_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var patient = A.Fake<Patient>();
            A.CallTo(() => _patientRepository.GetByIdAsync(id)).Returns(patient);
            // Act
            var result = _patientController.Detail(id);
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
