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

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Simple CRUD action to add a new address
        /// </summary>
        /// <param name="address">Address object to be added</param>
        /// <returns>Calls Save method to apply changes</returns>
        public bool Add(Address address)
        {
            _context.Add(address);
            return Save();
        }

        /// <summary>
        /// Simple CRUD action to delete Address
        /// </summary>
        /// <param name="address">Address object to delete</param>
        /// <returns>Calls Save method to apply changes</returns>
        public bool Delete(Address address)
        {
            _context.Remove(address);
            return Save();
        }

        /// <summary>
        /// Gets an async list of Address from a sql database and orders them by ID
        /// </summary>
        /// <returns>The Addresses List</returns>
        public async Task<IEnumerable<Address>> GetAll()
        {
            var addresses  = _context.Address
                .OrderBy(a => a.Id)
                .ToListAsync();

            return await addresses;

        }

        /// <summary>
        /// Gets a Specific Addres inside the Address List
        /// </summary>
        /// <param name="id">The ID to find the SPecific Address</param>
        /// <returns>Address Object</returns>
        public async Task<Address> GetByIdAsync(int id)
        {
            var address = _context.Address
                .Include(a => a.Patients)
                .Where(a => a.Id ==  id)
                .FirstOrDefaultAsync();

            return await address;
        }

        /// <summary>
        /// CRUD action to apply changes to DB
        /// </summary>
        /// <returns>A Bool. True if saved happend succefully of false othewise</returns>
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        /// <summary>
        /// CRUD action to update the values of a specific object
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool Update(Address address)
        {
            _context.Update(address);
            return Save();
        }

        public async Task<IEnumerable<string>> GetUniqueCountriesAsync()
        {
            var countries = _context.Address
                .Select(a => a.Country)
                .Distinct()
                .ToListAsync();

            return await countries;
        }
        public async Task<IEnumerable<string>> GetUniqueCitiesAsync()
        {
            var cities = _context.Address
                .Select(a => a.City)
                .Distinct()
                .ToListAsync();

            return await cities;
        }
        public async Task<IEnumerable<Address>> GetCityAsync(string city)
        {
            var addresses = _context.Address
                .Where(a => a.City == city)
                .ToListAsync();

            return await addresses;
        }
        public async Task<IEnumerable<Address>> GetCountryAsync(string country)
        {
            var addresses = _context.Address
                .Where(a => a.Country== country)
                .ToListAsync();

            return await addresses;
        }
    }
}
