using Bogus;
using BogusData.FakeModels;
using FluentAssertions;
using hospitals.Data;
using hospitals.Models;
using hospitals.Repository;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Tests.RepositoryTests
{
    public class DoctorRepositoryTests
    {
        FakeDoctor fd = new FakeDoctor();
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
                    databaseContext.Doctor.Add(
                        fd.GenerateDoctor()
                        );
                    await databaseContext.SaveChangesAsync();

                }
            }
            return databaseContext;
        }

        [Fact]
        public async void DoctorRepository_Add_ReturnsBool()
        {
            //Arrange
            var doctor = fd.GenerateDoctor();
            var dbContext = await GetDbContext();
            var doctorRepository = new DoctorRepository(dbContext);

            //Act
            var result = doctorRepository.Add(doctor);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void DoctorRepository_SaveNoChanges_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var doctorRepository = new DoctorRepository(dbContext);

            //Act
            var result = doctorRepository.Save();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void DoctorRepository_Delete_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var doctor = dbContext.Doctor.FirstOrDefault();
            var doctorRepository = new DoctorRepository(dbContext);

            //Act
            var result = doctorRepository.Delete(doctor);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void DoctorRepository_Update_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var doctor = dbContext.Doctor.FirstOrDefault();
            var doctorRepository = new DoctorRepository(dbContext);

            //Act
            var result = doctorRepository.Update(doctor);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void DoctorRepository_GetByIdAsync_ReturnsDoctor()
        {
            //Arrange
            int id = 1;
            var dbContext = await GetDbContext();
            var doctorRepository = new DoctorRepository(dbContext);

            //Act
            var result = doctorRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Doctor>>();
        }

        [Fact]
        public async void DoctorRepository_GetAll_ReturnsIEnumerableDoctors()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var doctorRepository = new DoctorRepository(dbContext);

            //Act
            var result = doctorRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Doctor>>>();
        }
    }
}
