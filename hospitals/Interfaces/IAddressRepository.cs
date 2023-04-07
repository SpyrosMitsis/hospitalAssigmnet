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
    }
}
