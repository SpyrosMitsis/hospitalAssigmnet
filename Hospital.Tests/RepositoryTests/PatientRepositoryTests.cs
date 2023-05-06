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
            if (await databaseContext.Patient.CountAsync() < 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Patient.Add(fp.GeneratePatient());
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void PatientRepository_Add_ReturnsBool()
        {
            //Arrange
            var patient = fp.GeneratePatient();
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
    }
}
