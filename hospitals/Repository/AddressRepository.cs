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
        public bool Add(Address address)
        {
            _context.Add(address);
            return Save();
        }

        public bool Delete(Address address)
        {
            _context.Remove(address);
            return Save();
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            var addresses  = _context.Address.OrderBy(a => a.Id).ToListAsync();
            return await addresses;

        }

        public async Task<Address> GetByIdAsync(int id)
        {
            var address = _context.Address.Include(a => a.Patients).Where(a => a.Id ==  id).FirstOrDefaultAsync();
            return await address;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Address address)
        {
            _context.Update(address);
            return Save();
        }
    }
}
