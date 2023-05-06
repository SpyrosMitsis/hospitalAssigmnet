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
    public class AddressRepositoryTests
    {
        FakeAddress fa = new FakeAddress();
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if( await databaseContext.Address.CountAsync() < 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Address.Add(
                        fa.GenerateAddress()
                        );
                    await databaseContext.SaveChangesAsync();

                }
            }
            return databaseContext;
        }

        [Fact]  
        public async void AddressRepository_Add_ReturnsBool()
        {
            //Arrange
            var address = fa.GenerateAddress();
            var dbContext = await GetDbContext();
            var addressRepository = new AddressRepository(dbContext);

            //Act
            var result = addressRepository.Add(address);

            //Assert
            result.Should().BeTrue();
        }
        [Fact]
        public async void AddressRepository_SaveNoChanges_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var addressRepository = new AddressRepository(dbContext);

            //Act
            var result = addressRepository.Save();

            //Assert
            result.Should().BeFalse();
        }
        [Fact]  
        public async void AddressRepository_Delete_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var address = dbContext.Address.FirstOrDefault();
            var addressRepository = new AddressRepository(dbContext);

            //Act
            var result = addressRepository.Delete(address);

            //Assert
            result.Should().BeTrue();
        }
        [Fact]  
        public async void AddressRepository_Update_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var address = dbContext.Address.FirstOrDefault();
            var addressRepository = new AddressRepository(dbContext);

            //Act
            var result = addressRepository.Update(address);

            //Assert
            result.Should().BeTrue();
        }
        [Fact]
        public async void AddressRepository_GetByIdAsync_ReturnsAddress()
        {
            //Arrange
            int id = 1;
            var dbContext = await GetDbContext();
            var addressRepository = new AddressRepository(dbContext);

            //Act
            var result = addressRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Address>>();
        }

        [Fact]
        public async void AddressRepository_GetAll_ReturnsIEnumerableAddresses()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var addressRepository = new AddressRepository(dbContext);

            //Act
            var result = addressRepository.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Address>>>();
        }

    }

}
