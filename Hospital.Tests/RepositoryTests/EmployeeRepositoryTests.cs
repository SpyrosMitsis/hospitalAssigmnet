using Bogus;
using BogusData.FakeModels;
using FluentAssertions;
using hospitals.Data;
using hospitals.Models;
using hospitals.Repository;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Grids.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.Tests.RepositoryTests
{
    public class EmployeeRepositoryTests
    {
        FakeEmployee fe = new FakeEmployee();
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Employee.CountAsync() < 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Employee.Add(
                        fe.GenerateEmployee()
                        );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        [Fact]
        public async void EmployeeRepository_Add_ReturnsBool()
        {
            //Arrange
            var employee = fe.GenerateEmployee();
            var dbContext = await GetDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);
        
            //Act
            var result = employeeRepository.Add<Employee>(employee);
        
            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void EmployeeRepository_SaveNoChanges_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);
        
            //Act
            var result = employeeRepository.Save();
        
            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void EmployeeRepository_Delete_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var employee = dbContext.Employee.FirstOrDefault();
            var employeeRepository = new EmployeeRepository(dbContext);
        
            //Act
            var result = employeeRepository.Delete<Employee>(employee);
        
            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void EmployeeRepository_Update_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var employee = dbContext.Employee.FirstOrDefault();
            var employeeRepository = new EmployeeRepository(dbContext);
        
            //Act
            var result = employeeRepository.Update<Employee>(employee);
        
            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void EmployeeRepository_GetByIdAsync_ReturnsEmployee()
        {
            //Arrange
            int id = 1;
            var dbContext = await GetDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);
        
            //Act
            var result = employeeRepository.GetByIdAsync<Employee>(id);
        
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Employee>>();
        }
        
        [Fact]
        public async void EmployeeRepository_GetAll_ReturnsIEnumerableEmployees()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);
        
            //Act
            var result = employeeRepository.GetAll<Employee>();
        
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Employee>>>();
        }

        [Fact]
        public async void EmployeeRepository_GetBySalary_ReturnsIEnumerableEmployee()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);
            float minSalary = 10000;
            float maxSalary = 70000;


            //Act
            var result = employeeRepository.GetEmployeeBySalary<Employee>(minSalary, maxSalary);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Employee>>>();
        }

        [Fact]
        public async void EmployeeRepository_GetByAge_ReturnsIEnumerableEmployee()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var employeeRepository = new EmployeeRepository(dbContext);
            int minAge = 30;
            int maxAge = 55;

            //Act
            var result = employeeRepository.GetEmployeeByAge<Employee>(minAge, maxAge);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Employee>>>();
        }

    }
}
