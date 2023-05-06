using FakeItEasy;
using FluentAssertions;
using hospitals.Controllers;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Tests.ControllerTests
{
    public class RoomControllerTests
    {
        private IRoomRepository _roomRepository;
        private RoomController _roomController;

        public RoomControllerTests()
        {
            
            // Dependencies
            _roomRepository = A.Fake<IRoomRepository>();
            // SUT
            _roomController= new RoomController(_roomRepository);
        }
        [Fact]
        public void RoomController_Index_ReturnsSuccess()
        {
            // Arrange
            var rooms = A.Fake<IEnumerable<Room>>();
            A.CallTo(() => _roomRepository.GetAll()).Returns(rooms);
            // Act
            var result = _roomController.Index();
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        [Fact]
        public void RoomController_Detail_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            var room = A.Fake<Room>();
            A.CallTo(() => _roomRepository.GetByIdAsync(id)).Returns(room);
            // Act
            var result = _roomController.Detail(id);
            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
