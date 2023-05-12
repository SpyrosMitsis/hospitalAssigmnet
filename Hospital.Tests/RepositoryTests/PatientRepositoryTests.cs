using Bogus;
using BogusData.FakeModels;
using FluentAssertions;
using hospitals.Data;
using hospitals.Models;
using hospitals.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Syncfusion.Blazor.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Tests.RepositoryTests
{
    public class PatientRepositoryTests
    {
        FakePatient fp = new FakePatient();

        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Patient.CountAsync() < 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Patient.Add(
                        fp.GeneratePatient()
                        );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void PatientRepository_Delete_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patient = dbContext.Patient.FirstOrDefault();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.Delete(patient);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void PatientRepository_Update_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patient = dbContext.Patient.FirstOrDefault();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.Update(patient);

            //Assert
            result.Should().BeTrue();
        }
        [Fact]
        public async void PatientRepository_Add_ReturnsBool()
        {
            //Arrange
            var patient= fp.GeneratePatient();
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.Add(patient);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void PatientRepository_SaveNoChanges_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.Save();

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void PatientRepository_GetByAge_ReturnsIEnumerablePatients()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);
            int minAge = 30;
            int maxAge = 55;

            //Act
            var result = patientRepository.GetPatientByAge(minAge, maxAge);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Patient>>>();
        }

        [Fact]
        public async void PatientRepository_GetAllByAddress_ReturnsIEnumerableIGrouping()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.GetAllByAddress();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<IGrouping<Address,Patient>>>>();
        }
        [Fact]
        public async void PatientRepository_GetAllByDoctor_ReturnsIEnumerableIGrouping()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.GetAllByDoctor();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<IGrouping<Doctor,Patient>>>>();
        }

        [Fact]
        public async void PatientRepository_GetAllByRoom_ReturnsIEnumerableIGrouping()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.GetAllByRoom();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<IGrouping<Room,Patient>>>>();
        }

        [Fact]
        public async void PatientRepository_GetAll_ReturnsIEnumerablePatients()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Patient>>>();
        }

        [Fact]
        public async void PatientRepository_GetAllWIthAddress_ReturnsIEnumerablePatients()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.GetAllWithAddresses();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Patient>>>();
        }
        [Fact]
        public async void PatientRepository_GetByDate_ReturnsIEnumerablePatients()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);
            DateTime entryDate = DateTime.Now;
            DateTime exitDate = DateTime.Now.AddDays(1);

            //Act
            var result = patientRepository.GetPatientByDate(entryDate, exitDate);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Patient>>>();
        }
        [Fact]
        public async void PatientRepository_GetByIdAsync_ReturnsPatient()
        {
            //Arrange
            int id = 1;
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Patient>>();
        }
        [Fact]
        public async void PatientRepository_GetByMinAgeAsync_ReturnsInt()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.GetMinAgeAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<int>>();
        }
        [Fact]
        public async void PatientRepository_GetByMaxAgeAsync_ReturnsInt()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var patientRepository = new PatientRepository(dbContext);

            //Act
            var result = patientRepository.GetMaxAgeAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<int>>();
        }
    }
}
