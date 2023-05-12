using BogusData.FakeModels;
using FluentAssertions;
using hospitals.Data;
using hospitals.Models;
using hospitals.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Tests.RepositoryTests
{
    public class RoomRepositoryTests
    {
        FakeRoom fr = new FakeRoom();
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Doctor.CountAsync() < 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Room.Add(
                        fr.GenerateRoom()
                        );
                    await databaseContext.SaveChangesAsync();

                }
            }
            return databaseContext;
        }

        [Fact]
        public async void RoomRepository_Add_ReturnsBool()
        {
            //Arrange
            var room = fr.GenerateRoom();
            var dbContext = await GetDbContext();
            var roomRepository = new RoomRepository(dbContext);

            //Act
            var result = roomRepository.Add(room);

            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void RoomRepository_SaveNoChanges_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var roomRepository = new RoomRepository(dbContext);

            //Act
            var result = roomRepository.Save();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void RoomRepository_Delete_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var room = dbContext.Room.FirstOrDefault();
            var roomRepository = new RoomRepository(dbContext);

            //Act
            var result = roomRepository.Delete(room);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void RoomRepository_Update_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var room = dbContext.Room.FirstOrDefault();
            var roomRepository = new RoomRepository(dbContext);

            //Act
            var result = roomRepository.Update(room);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void RoomRepository_GetByIdAsync_ReturnsRoom()
        {
            //Arrange
            int id = 1;
            var dbContext = await GetDbContext();
            var roomRepository = new RoomRepository(dbContext);

            //Act
            var result = roomRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Room>>();
        }

        [Fact]
        public async void RoomRepository_GetAll_ReturnsIEnumerableRooms()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var roomRepository = new RoomRepository(dbContext);

            //Act
            var result = roomRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Room>>>();
        }
        [Fact]
        public async void RoomRepository_GetUniqueFloors_ReturnsIEnumerableString()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var roomRepository = new RoomRepository(dbContext);

            //Act
            var result = roomRepository.GetUniqueFloorNames();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<string>>>();
        }
    }
}




















