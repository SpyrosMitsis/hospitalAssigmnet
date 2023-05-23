using Azure.Core;
using hospitals.Data;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository"/> class.
        /// </summary>
        /// <param name="context">The application DbContext.</param>
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        /// <summary>
        /// Adds an address to the repository.
        /// </summary>
        /// <param name="address">The address to add.</param>
        /// <returns>True if the address is added successfully, false otherwise.</returns>
        public bool Add(Address address)
        {
            _context.Add(address);
            return Save();
        }
    
        /// <summary>
        /// Deletes an address from the repository.
        /// </summary>
        /// <param name="address">The address to delete.</param>
        /// <returns>True if the address is deleted successfully, false otherwise.</returns>
        public bool Delete(Address address)
        {
            _context.Remove(address);
            return Save();
        }
    
        /// <summary>
        /// Gets all addresses from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of addresses.</returns>
        public async Task<IEnumerable<Address>> GetAll()
        {
            var addresses = await _context.Address
                .OrderBy(a => a.Id)
                .ToListAsync();
    
            return addresses;
        }
    
        /// <summary>
        /// Gets an address by its ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the address to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the address.</returns>
        public async Task<Address> GetByIdAsync(int id)
        {
            var address = await _context.Address
                .Include(a => a.Patients)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
    
            return address;
        }
    
        /// <summary>
        /// Saves changes made in the repository.
        /// </summary>
        /// <returns>True if the changes are saved successfully, false otherwise.</returns>
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    
        /// <summary>
        /// Updates an address in the repository.
        /// </summary>
        /// <param name="address">The address to update.</param>
        /// <returns>True if the address is updated successfully, false otherwise.</returns>
        public bool Update(Address address)
        {
            _context.Update(address);
            return Save();
        }
    
        /// <summary>
        /// Gets a collection of unique countries from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of unique country names.</returns>
        public async Task<IEnumerable<string>> GetUniqueCountriesAsync()
        {
            var countries = await _context.Address
                .Select(a => a.Country)
                .Distinct()
                .ToListAsync();
    
            return countries;
        }
    
        /// <summary>
        /// Gets a collection of unique cities from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of unique city names.</returns>
        public async Task<IEnumerable<string>> GetUniqueCitiesAsync()
        {
            var cities = await _context.Address
                .Select(a => a.City)
                .Distinct()
                .ToListAsync();
    
            return cities;
        }
    
        /// <summary>
        /// Gets addresses filtered by a specific city from the repository.
        /// </summary>
        /// <param name="city">The city to filter addresses by.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of filtered addresses.</returns>
        public async Task<IEnumerable<Address>> GetCityAsync(string city)
        {
            var addresses = await _context.Address
                .Where(a => a.City == city)
                .ToListAsync();
    
            return addresses;
        }
    
        /// <summary>
        /// Gets addresses filtered by a specific country from the repository.
        /// </summary>
        /// <param name="country">The country to filter addresses by.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of filtered addresses.</returns>
        public async Task<IEnumerable<Address>> GetCountryAsync(string country)
        {
            var addresses = await _context.Address
                .Where(a => a.Country == country)
                .ToListAsync();
    
            return addresses;
        }
    }
}
