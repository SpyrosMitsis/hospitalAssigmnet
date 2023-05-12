using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAll();
        Task<Address> GetByIdAsync(int id);
        bool Add(Address address);
        bool Update(Address address);
        bool Delete(Address address);
        bool Save();
        Task<IEnumerable<string>> GetUniqueCountriesAsync();
        Task<IEnumerable<string>> GetUniqueCitiesAsync();
        Task<IEnumerable<Address>> GetCityAsync(string city);
        Task<IEnumerable<Address>> GetCountryAsync(string country);
    }
}
