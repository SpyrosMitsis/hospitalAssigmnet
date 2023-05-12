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
    public class NurseRepositoryTests
    {
        FakeNurse fn = new FakeNurse();
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Nurse.CountAsync() < 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Nurse.Add(
                        fn.GenerateNurse()
                        );
                    await databaseContext.SaveChangesAsync();

                }
            }
            return databaseContext;
        }
        
        [Fact]
        public async void NurseRepository_GetByIdAsync_ReturnsNurse()
        {
            //Arrange
            int id = 1;
            var dbContext = await GetDbContext();
            var nurseRepository = new NurseRepository(dbContext);
        
            //Act
            var result = nurseRepository.GetByIdAsync(id);
        
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Nurse>>();
        }
    }
}
